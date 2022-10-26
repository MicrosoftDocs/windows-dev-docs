import http.client
import json
import requests

class DevCenterAccessTokenClient(object):
    """A client for acquiring access tokens from AAD to use with the Dev Center Client."""
    def __init__(self, tenant_id, client_id, client_secret):
        self.tenant_id = tenant_id
        self.client_id = client_id
        self.client_secret = client_secret

    def get_access_token(self, resource):
        """Acquires an access token to the specific resource via the AAD tenant."""
        body_format = "grant_type=client_credentials&client_id={0}&client_secret={1}&resource={2}"
        body = body_format.format(self.client_id, self.client_secret, resource)
        access_headers = {"Content-Type": "application/x-www-form-urlencoded; charset=utf-8"}
        token_conn = http.client.HTTPSConnection("login.microsoftonline.com")
        token_relative_path = "/{0}/oauth2/token".format(self.tenant_id)
        token_conn.request("POST", token_relative_path, body, headers=access_headers)

        token_response = token_conn.getresponse()
        token_json = json.loads(token_response.read().decode())
        token_conn.close()
        return token_json["access_token"]

class DevCenterClient(object):
    """A client for the Dev Center API."""
    def __init__(self, base_uri, access_token):
        self.base_uri = base_uri
        self.request_headers = {
            "Authorization": "Bearer " + access_token,
            "Content-type": "application/json",
            "User-Agent": "Python"
        }

    def get_application(self, application_id):
        """Returns the application as defined in Dev Center."""
        path = "/v1.0/my/applications/{0}".format(application_id)
        return self._get(path)

    def cancel_in_progress_submission(self, application_id, submission_id):
        """Cancels the in-progress submission."""
        path = "/v1.0/my/applications/{0}/submissions/{1}".format(application_id, submission_id)
        return self._delete(path)

    def create_submission(self, application_id):
        """Creates a new submission in Dev Center. This is identical to clicking
        the Create Submission button in Dev Center."""
        path = "/v1.0/my/applications/{0}/submissions".format(application_id)
        return self._post(path)

    def update_submission(self, application_id, submission_id, submission):
        """Updates the submission in Dev Center using the JSON provided."""
        path = "/v1.0/my/applications/{0}/submissions/{1}"
        path = path.format(application_id, submission_id)
        return self._put(path, submission)
    
    def get_submission(self, application_id, submission_id):
        """Gets the submission in Dev Center."""
        path = "/v1.0/my/applications/{0}/submissions/{1}"
        path = path.format(application_id, submission_id)
        return self._get(path)

    def commit_submission(self, application_id, submission_id):
        """Commits the submission to Dev Center. Once committed, Dev Center will
        begin processing the submission and verify package integrity and send
        it for certification."""
        path = "/v1.0/my/applications/{0}/submissions/{1}/commit"
        path = path.format(application_id, submission_id)
        return self._post(path)

    def get_submission_status(self, application_id, submission_id):
        """Returns the current state of the submission in Dev Center,
        such as is the submission in certification, committed, publishing,
        etc."""
        path = "/v1.0/my/applications/{0}/submissions/{1}/status"
        path = path.format(application_id, submission_id)
        response_ok, response_obj = self._get(path)
        if "status" in response_obj:
            return (response_ok, response_obj["status"])
        else:
            return (response_ok, "Unknown")

    def upload_zip_file_for_submission(self, application_id, submission_id, zip_file_path):
        """Uploads a ZIP file for the Submission API for the submission object."""
        is_ok, submission = self.get_submission(application_id, submission_id)
        if not is_ok:
            raise "Failed to get submission."

        zip_file = open(zip_file_path, 'rb')
        upload_uri = submission["fileUploadUrl"].replace("+", "%2B")
        upload_headers = {"x-ms-blob-type": "BlockBlob"}
        upload_response = requests.put(upload_uri, zip_file, headers=upload_headers)
        upload_response.raise_for_status()

    def _get(self, path):
        return self._invoke("GET", path)

    def _post(self, path, obj=None):
        return self._invoke("POST", path, obj)

    def _put(self, path, obj=None):
        return self._invoke("PUT", path, obj)

    def _delete(self, path):
        return self._invoke("DELETE", path)

    def _invoke(self, method, path, obj=None):
        body = ""
        if not obj is None:
            body = json.dumps(obj)
        conn = http.client.HTTPSConnection(self.base_uri)
        conn.request(method, path, body, self.request_headers)
        response = conn.getresponse()
        response_body = response.read().decode()
        response_body_length = int(response.headers["Content-Length"])
        response_obj = None
        if not response_body is None and response_body_length != 0:
            response_obj = json.loads(response_body)
        response_ok = self._response_ok(response)
        conn.close()
        return (response_ok, response_obj)

    def _response_ok(self, response):
        status_code = int(response.status)
        return status_code >= 200 and status_code <= 299
