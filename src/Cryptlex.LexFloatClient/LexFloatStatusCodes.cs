using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptlex
{
    public static class LexFloatStatusCodes
    {
        /*
            CODE: LF_OK

            MESSAGE: Success code.
        */
        public const int LF_OK = 0;

        /*
            CODE: LF_FAIL

            MESSAGE: Failure code.
        */
        public const int LF_FAIL = 1;

        /*
            CODE: LF_E_PRODUCT_ID

            MESSAGE: The product id is incorrect.
        */
        public const int LF_E_PRODUCT_ID = 40;

        /*
            CODE: LF_E_CALLBACK

            MESSAGE: Invalid or missing callback function.
        */
        public const int LF_E_CALLBACK = 41;

        /*
            CODE: LF_E_HOST_URL

            MESSAGE: Missing or invalid server url.
        */
        public const int LF_E_HOST_URL = 42;

        /*
            CODE: LF_E_TIME

            MESSAGE: Ensure system date and time settings are correct.
        */
        public const int LF_E_TIME = 43;

        /*
            CODE: LF_E_INET

            MESSAGE: Failed to connect to the server due to network error.
        */
        public const int LF_E_INET = 44;

        /*
            CODE: LF_E_NO_LICENSE

            MESSAGE: License has not been leased yet.
        */
        public const int LF_E_NO_LICENSE = 45;

        /*
            CODE: LF_E_LICENSE_EXISTS

            MESSAGE: License has already been leased.
        */
        public const int LF_E_LICENSE_EXISTS = 46;

        /*
            CODE: LF_E_LICENSE_NOT_FOUND

            MESSAGE: License does not exist on server or has already expired. This
            happens when the request to refresh the license is delayed.
        */
        public const int LF_E_LICENSE_NOT_FOUND = 47;

        /*
            CODE: LF_E_LICENSE_EXPIRED_INET

            MESSAGE: License lease has expired due to network error. This
            happens when the request to refresh the license fails due to
            network error.
        */
        public const int LF_E_LICENSE_EXPIRED_INET = 48;

        /*
            CODE: LF_E_LICENSE_LIMIT_REACHED

            MESSAGE: The server has reached it's allowed limit of floating licenses.
        */
        public const int LF_E_LICENSE_LIMIT_REACHED = 49;

        /*
            CODE: LF_E_BUFFER_SIZE

            MESSAGE: The buffer size was smaller than required.
        */
        public const int LF_E_BUFFER_SIZE = 50;

        /*
            CODE: LF_E_METADATA_KEY_NOT_FOUND

            MESSAGE: The metadata key does not exist.
        */
        public const int LF_E_METADATA_KEY_NOT_FOUND = 51;

        /*
            CODE: LF_E_METADATA_KEY_LENGTH

            MESSAGE: Metadata key length is more than 256 characters.
        */
        public const int LF_E_METADATA_KEY_LENGTH = 52;

        /*
            CODE: LF_E_METADATA_VALUE_LENGTH

            MESSAGE: Metadata value length is more than 256 characters.
        */
        public const int LF_E_METADATA_VALUE_LENGTH = 53;

        /*
            CODE: LF_E_ACTIVATION_METADATA_LIMIT

            MESSAGE: The floating client has reached it's metadata fields limit.
        */
        public const int LF_E_FLOATING_CLIENT_METADATA_LIMIT = 54;

        /*
            CODE: LF_E_METER_ATTRIBUTE_NOT_FOUND

            MESSAGE: The meter attribute does not exist.
        */
        public const int LF_E_METER_ATTRIBUTE_NOT_FOUND = 55;

        /*
            CODE: LF_E_METER_ATTRIBUTE_USES_LIMIT_REACHED

            MESSAGE: The meter attribute has reached it's usage limit.
        */
        public const int LF_E_METER_ATTRIBUTE_USES_LIMIT_REACHED = 56;

        /*
            CODE: LF_E_PRODUCT_VERSION_NOT_LINKED

            MESSAGE: No product version is linked with the license.
        */
        public const int LF_E_PRODUCT_VERSION_NOT_LINKED = 57;

        /*
            CODE: LF_E_FEATURE_FLAG_NOT_FOUND

            MESSAGE: The product version feature flag does not exist.
        */
        public const int LF_E_FEATURE_FLAG_NOT_FOUND = 58;

        /*
            CODE: LF_E_IP

            MESSAGE: IP address is not allowed.
        */
        public const int LF_E_IP = 60;

        /*
            CODE: LF_E_CLIENT

            MESSAGE: Client error.
        */
        public const int LF_E_CLIENT = 70;

        /*
            CODE: LF_E_SERVER

            MESSAGE: Server error.
        */
        public const int LF_E_SERVER = 71;

        /*
            CODE: LF_E_SERVER_TIME_MODIFIED

            MESSAGE: System time on server has been tampered with. Ensure
            your date and time settings are correct on the server machine.
        */
        public const int LF_E_SERVER_TIME_MODIFIED = 72;

        /*
            CODE: LF_E_SERVER_LICENSE_NOT_ACTIVATED

            MESSAGE: The server has not been activated using a license key.
        */
        public const int LF_E_SERVER_LICENSE_NOT_ACTIVATED = 73;

        /*
            CODE: LF_E_SERVER_LICENSE_EXPIRED

            MESSAGE: The server license has expired.
        */
        public const int LF_E_SERVER_LICENSE_EXPIRED = 74;

        /*
            CODE: LF_E_SERVER_LICENSE_SUSPENDED

            MESSAGE: The server license has been suspended.
        */
        public const int LF_E_SERVER_LICENSE_SUSPENDED = 75;

        /*
            CODE: LF_E_SERVER_LICENSE_GRACE_PERIOD_OVER

            MESSAGE: The grace period for server license is over.
        */
        public const int LF_E_SERVER_LICENSE_GRACE_PERIOD_OVER = 76;
    }
}
