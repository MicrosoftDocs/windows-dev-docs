def get_listings_object():
    """Gets a sample listings map for a submission."""
    listings = {
        # Each listing is targeted at a specific language-locale code, e.g. EN-US.
        "en-us" : {
            # This structure holds basic information to display in the store.
            "baseListing" : {
                "copyrightAndTrademarkInfo" : "(C) 2017 Microsoft",
                # Up to 7 keywords may be provided in a listing.
                "keywords"  : ["SampleApp", "SampleFightingGame", "GameOptions"],
                "licenseTerms" : "http://example.com/licenseTerms.aspx",
                "privacyPolicy" : "http://example.com/privacyPolicy.aspx",
                "supportContact" : "support@example.com",
                "websiteUrl" : "http://example.com",
                "description" : "A sample game showing off gameplay options code.",
                "features" : ["Doesn't crash", "Likes to eat chips"],
                "releaseNotes" : "Initial release",
                "recommendedHardware" : [],
                # If your app works better with specific hardware (or needs it), you can
                # add or update values here.
                "hardwarePreferences": ["Keyboard", "Mouse"],
                # The title of the app must match a reserved name for the app in Dev Center.
                # If it doesn't, attempting to update the submission will fail.
                "title" : "Super Dev Center API Simulator 2017",
                "images" : [
                    # There are several types of images available; at least one screenshot
                    # is required.
                    {
                        # The file name is relative to the root of the uploaded ZIP file.
                        "fileName" : "img/screenshot.png",
                        "description" : "A basic screenshot of the app.",
                        "imageType" : "Screenshot"
                    }
                ]
            },
            # If there are any specific overrides to above information for Windows 8,
            # Windows 8.1, Windows Phone 7.1, 8.0, or 8.1, you can add information here.
            "platformOverrides" : {}
        }
    }
    return listings

def get_package_object():
    """Gets a sample package for the submission in Dev Center."""
    package = {
        # The file name is relative to the root of the uploaded ZIP file.
        "fileName" : "bin/super_dev_ctr_api_sim.appxupload",
        # If you haven't begun to upload the file yet, set this value to "PendingUpload".
        "fileStatus" : "PendingUpload"
    }
    return package

def get_pricing_object():
    """Gets a sample pricing object for a submission."""
    pricing = {
        # How long the trial period is, if one is allowed. Valid values are NoFreeTrial,
        # OneDay, SevenDays, FifteenDays, ThirtyDays, or TrialNeverExpires.
        "trialPeriod" : "NoFreeTrial",
        # Maps to the default price for the app.
        "priceId" : "Free",
        # If you'd like to offer your app in different markets at different prices, you
        # can provide priceId values per language/locale code.
        "marketSpecificPricing" : {}
    }
    return pricing

def get_device_families_object():
    """Gets a sample device families object for a submission."""
    device_families = {
        # Supported values are Desktop, Mobile, Xbox, and Holographic. To make
        # the app available on that specific platform, set the value to True.
        "Desktop" : True,
        "Mobile" : False,
        "Xbox" : True,
        "Holographic" : False
    }
    return device_families

def get_gaming_options_object():
    """Gets a sample gaming options object for a submission."""
    gaming_options = {
        # The genres of your app.
        "Genres" : ["Games_Fighting"],

        # Set this to True if your game supports local multiplayer. This field is required.
        "IsLocalMultiplayer" : True,

        # If local multiplayer is supported, you must provide the minimum and maximum players
        # supported. Valid values are between 2 and 1000 inclusive.
        "LocalMultiplayerMinPlayers" : 2,
        "LocalMultiplayerMaxPlayers" : 4,

        # Set this to True if your game supports local co-op play. This field is required.
        "IsLocalCooperative" : True,

        # If local co-op is supported, you must provide the minimum and maximum players
        # supported. Valid values are between 2 and 1000 inclusive.
        "LocalCooperativeMinPlayers" : 2,
        "LocalCooperativeMaxPlayers" : 4,

        # Set this to True if your game supports online multiplayer. This field is required.
        "IsOnlineMultiplayer" : True,

        # If online multiplayer is supported, you must provide the minimum and maximum players
        # supported. Valid values are between 2 and 1000 inclusive.
        "OnlineMultiplayerMinPlayers" : 2,
        "OnlineMultiplayerMaxPlayers" : 4,

        # Set this to true if your game supports online co-op play. This field is required.
        "IsOnlineCooperative" : True,

        # If online co-op is supported, you must provide the minimum and maximum players
        # supported. Valid values are between 2 and 1000 inclusive.
        "OnlineCooperativeMinPlayers" : 2,
        "OnlineCooperativeMaxPlayers" : 4,

        # If your game supports broadcasting a stream to other players, set this field to True.
        # The field is required.
        "IsBroadcastingPrivilegeGranted" : True,

        # If your game supports cross-device play (e.g. a player can play on an Xbox One with
        # their friend who's playing on a PC), set this field to True. This field is required.
        "IsCrossPlayEnabled" : True,

        # If your game supports Kinect usage, set this field to "Enabled", otherwise, set it to
        # "Disabled". This field is required.
        "KinectDataForExternal" : "Disabled",

        # Free text about any other peripherals that your game supports. This field is optional.
        "OtherPeripherals" : "Supports the usage of all fighting joysticks."
    }
    return gaming_options

def get_trailer_object():
    """Gets a sample trailer object for the submission in Dev Center."""
    trailer = {
        # This is the filename of the trailer. The file name is a relative path to the
        # root of the ZIP file to be uploaded to the API.
        "VideoFileName" : "trailers/main/my_awesome_trailer.mpeg",

        # Aside from the video itself, a trailer can have image assets such as screenshots
        # or alternate images. These are separated by language-locale code, e.g. EN-US.
        "TrailerAssets" : {
            "en-us" : {

                # The title of the trailer to display in the store.
                "Title" : "Main Trailer",

                # The list of images provided with the trailer that are shown
                # when the trailer isn't playing.
                "ImageList" : [
                    {
                        # The file name of the image. The file name is a relative
                        # path to the root of the ZIP
                        # file to be uploaded to the API.
                        "FileName" : "trailers/main/thumbnail.png",

                        # A plaintext description of what the image represents.
                        "Description" : "The thumbnail for the trailer shown " +
                                        "before the user clicks play"
                    },
                    {
                        "FileName" : "trailers/main/alt-img.png",
                        "Description" : "The image to show after the trailer plays"
                    }
                ]
            }
        }
    }
    return trailer
