import time
from devcenterclient import DevCenterClient, DevCenterAccessTokenClient
import submissiondatasamples as samples

# Add your tenant ID, client ID, and client secret here.
tenant = ""
client = ""
secret = ""
acc_token_client = DevCenterAccessTokenClient(tenant, client, secret)

acc_token = acc_token_client.get_access_token("https://manage.devcenter.microsoft.com")
dev_center = DevCenterClient("manage.devcenter.microsoft.com", acc_token)

# The application ID is taken from your app dashboard page's URI in Dev Center,
# e.g. https://developer.microsoft.com/en-us/dashboard/apps/{application_id}/
application_id = ""

# Get the application object, and cancel any in progress submissions.
is_ok, app = dev_center.get_application(application_id)
if is_ok:
    if "pendingApplicationSubmission" in app:
        in_progress_submission_id = app["pendingApplicationSubmission"]["id"]
        is_ok = dev_center.cancel_in_progress_submission(application_id, in_progress_submission_id)
        if not is_ok:
            print("Failed to cancel the in-progress submission.")
            # Perform error handling or exit the script
else:
    print("Failed to retrieve the application object.")
    # Perform error handling or exit the script

# Create a new submission, based on the last published submission.
is_ok, submission = dev_center.create_submission(application_id)
assert is_ok
submission_id = submission["id"]

# The following fields are required:
submission["applicationCategory"] = "Games_Fighting"
submission["listings"] = samples.get_listings_object()
submission["Pricing"] = samples.get_pricing_object()
submission["packages"] = [samples.get_package_object()]
submission["allowTargetFutureDeviceFamilies"] = samples.get_device_families_object()

# The app must have the hasAdvancedListingPermission set to True in order for gaming options
# and trailers to be applied. If that's not the case, you can still update the app and
# its submissions through the API, but gaming options and trailers won't be saved.
if not "hasAdvancedListingPermission" in app or not app["hasAdvancedListingPermission"]:
    print("This application does not support gaming options or trailers.")
else:
    submission["gamingOptions"] = [samples.get_gaming_options_object()]
    submission["trailers"] = [samples.get_trailer_object()]

# Continue updating the submission_json object with additional options as needed.
# After you've finished, call the Update API with the code below to save it:
is_ok, submission = dev_center.update_submission(application_id, submission_id, submission)
assert is_ok

# All images and packages should be located in a single ZIP file. In the submission JSON, 
# the file names for all objects requiring them (icons, packages, etc.) must exactly 
# match the file names from the ZIP file.
zip_file_path = ""
is_ok = dev_center.upload_zip_file_for_submission(application_id, submission_id, zip_file_path)
assert is_ok

# Committing the submission will start the submission process for it. Once committed,
# the submission can no longer be changed.
is_ok = dev_center.commit_submission(application_id, submission_id)
assert is_ok

# After committing, you can poll the commit API for the status of the submission's process using
# the following code.
waiting_for_commit_start = True
while waiting_for_commit_start:
    is_ok, submission_status = dev_center.get_submission_status(application_id, submission_id)
    assert is_ok
    waiting_for_commit_start = submission_status == "CommitStarted"
    if waiting_for_commit_start:
        time.sleep(60)


