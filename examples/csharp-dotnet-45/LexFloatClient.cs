using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;

namespace Cryptlex
{
    public static class LexFloatClient
    {
        private const string DLL_FILE_NAME = "LexFloatClient.dll";

        /*
            In order to use "Any CPU" configuration, rename 64 bit LexFloatClient.dll to LexFloatClient64.dll and add "LF_ANY_CPU"
	        conditional compilation symbol in your project properties.
        */
#if LF_ANY_CPU
        private const string DLL_FILE_NAME_X64 = "LexFloatClient64.dll";
#endif

        /*
            FUNCTION: SetHostProductId()

            PURPOSE: Sets the product id of your application.

            PARAMETERS:
            * productId - the unique product id of your application as mentioned
            on the product page in the dashboard.

            RETURN CODES: LF_OK, LF_E_PRODUCT_ID
        */
        public static int SetHostProductId(string productId)
        {
#if LF_ANY_CPU
            return IntPtr.Size == 8 ? Native.SetHostProductId_x64(productId) : Native.SetHostProductId(productId);
#else
            return Native.SetHostProductId(productId);
#endif
        }

        /*
            FUNCTION: SetHostUrl()

            PURPOSE: Sets the network address of the LexFloatServer.

            The url format should be: http://[ip or hostname]:[port]

            PARAMETERS:
            * hostUrl - url string having the correct format

            RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_HOST_URL
        */
        public static int SetHostUrl(string hostUrl)
        {
#if LF_ANY_CPU
            return IntPtr.Size == 8 ? Native.SetHostUrl_x64(hostUrl) : Native.SetHostUrl(hostUrl);
#else
            return Native.SetHostUrl(hostUrl);
#endif
        }

        /*
            FUNCTION: SetFloatingLicenseCallback()

            PURPOSE: Sets the renew license callback function.

            Whenever the license lease is about to expire, a renew request is sent to the
            server. When the request completes, the license callback function
            gets invoked with one of the following status codes:

            LF_OK, LF_E_INET, LF_E_LICENSE_EXPIRED_INET, LF_E_LICENSE_NOT_FOUND, LF_E_CLIENT, LF_E_IP,
            LF_E_SERVER, LF_E_TIME, LF_E_SERVER_LICENSE_NOT_ACTIVATED,LF_E_SERVER_TIME_MODIFIED,
            LF_E_SERVER_LICENSE_SUSPENDED, LF_E_SERVER_LICENSE_EXPIRED, LF_E_SERVER_LICENSE_GRACE_PERIOD_OVER

            PARAMETERS:
            * callback - name of the callback function

            RETURN CODES: LF_OK, LF_E_PRODUCT_ID
        */
        public static int SetFloatingLicenseCallback(CallbackType callback)
        {
            var wrappedCallback = callback;
            var syncTarget = callback.Target as System.Windows.Forms.Control;
            if (syncTarget != null)
            {
                wrappedCallback = (v) => syncTarget.Invoke(callback, new object[] { v });
            }
            callbackList.Add(wrappedCallback);
#if LF_ANY_CPU
            return IntPtr.Size == 8 ? Native.SetFloatingLicenseCallback_x64(wrappedCallback) : Native.SetFloatingLicenseCallback(wrappedCallback);
#else
            return Native.SetFloatingLicenseCallback(wrappedCallback);
#endif
        }

        /*
            FUNCTION: SetFloatingClientMetadata()

            PURPOSE: Sets the floating client metadata.

            The  metadata appears along with the license details of the license
            in LexFloatServer dashboard.

            PARAMETERS:
            * key - string of maximum length 256 characters with utf-8 encoding.
            * value - string of maximum length 256 characters with utf-8 encoding.

            RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_METADATA_KEY_LENGTH,
            LF_E_METADATA_VALUE_LENGTH, LF_E_ACTIVATION_METADATA_LIMIT
        */
        public static int SetFloatingClientMetadata(string key, string value)
        {
#if LF_ANY_CPU
            return IntPtr.Size == 8 ? Native.SetFloatingClientMetadata_x64(key, value) : Native.SetFloatingClientMetadata(key, value);
#else
            return Native.SetFloatingClientMetadata(key, value);
#endif
        }

        /*
            FUNCTION: GetHostLicenseMetadata()

            PURPOSE: Get the value of the license metadata field associated with the LexFloatServer license.

            PARAMETERS:
            * key - key of the metadata field whose value you want to get
            * value - pointer to a buffer that receives the value of the string
            * length - size of the buffer pointed to by the value parameter

            RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE, LF_E_BUFFER_SIZE,
            LF_E_METADATA_KEY_NOT_FOUND
        */
        public static int GetHostLicenseMetadata(string key, StringBuilder value, int length)
        {
#if LF_ANY_CPU
            return IntPtr.Size == 8 ? Native.GetHostLicenseMetadata_x64(key, value, length) : Native.GetHostLicenseMetadata(key, value, length);
#else
            return Native.GetHostLicenseMetadata(key, value, length);
#endif
        }

        /*
            FUNCTION: GetHostLicenseMeterAttribute()

            PURPOSE: Gets the license meter attribute allowed uses and total uses associated with the LexFloatServer license.

            PARAMETERS:
            * name - name of the meter attribute
            * allowedUses - pointer to the integer that receives the value
            * totalUses - pointer to the integer that receives the value

            RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE, LF_E_METER_ATTRIBUTE_NOT_FOUND
        */
        public static int GetHostLicenseMeterAttribute(string name, ref uint allowedUses, ref uint totalUses)
        {
#if LF_ANY_CPU
            return IntPtr.Size == 8 ? Native.GetHostLicenseMeterAttribute_x64(name, ref allowedUses, ref totalUses) : Native.GetHostLicenseMeterAttribute(name, ref allowedUses, ref totalUses);
#else 
            return Native.GetHostLicenseMeterAttribute(name, ref allowedUses, ref totalUses);
#endif
        }

        /*
            FUNCTION: GetHostLicenseExpiryDate()

            PURPOSE: Gets the license expiry date timestamp of the LexFloatServer license.

            PARAMETERS:
            * expiryDate - pointer to the integer that receives the value

            RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE
        */
        public static int GetHostLicenseExpiryDate(ref uint expiryDate)
        {
#if LF_ANY_CPU
            return IntPtr.Size == 8 ? Native.GetHostLicenseExpiryDate_x64(ref expiryDate) : Native.GetHostLicenseExpiryDate(ref expiryDate);
#else
            return Native.GetHostLicenseExpiryDate(ref expiryDate);
#endif
        }

        /*
            FUNCTION: GetFloatingClientMeterAttributeUses()

            PURPOSE: Gets the meter attribute uses consumed by the floating client.

            PARAMETERS:
            * name - name of the meter attribute
            * uses - pointer to the integer that receives the value

            RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE, LF_E_METER_ATTRIBUTE_NOT_FOUND
        */
        public static int GetFloatingClientMeterAttributeUses(string name, ref uint uses)
        {
#if LF_ANY_CPU
            return IntPtr.Size == 8 ? Native.GetFloatingClientMeterAttributeUses_x64(name, ref uses) : Native.GetFloatingClientMeterAttributeUses(name, ref uses);
#else 
            return Native.GetFloatingClientMeterAttributeUses(name, ref uses);
#endif
        }

        /*
            FUNCTION: RequestFloatingLicense()

            PURPOSE: Sends the request to lease the license from the LexFloatServer.

            RETURN CODES: LF_OK, LF_FAIL, LF_E_PRODUCT_ID, LF_E_LICENSE_EXISTS, LF_E_HOST_URL,
            LF_E_CALLBACK, LF_E_LICENSE_LIMIT_REACHED, LF_E_INET, LF_E_TIME, LF_E_CLIENT, LF_E_IP, LF_E_SERVER,
            LF_E_SERVER_LICENSE_NOT_ACTIVATED, LF_E_SERVER_TIME_MODIFIED, LF_E_SERVER_LICENSE_SUSPENDED,
            LF_E_SERVER_LICENSE_GRACE_PERIOD_OVER, LF_E_SERVER_LICENSE_EXPIRED
        */
        public static int RequestFloatingLicense()
        {
#if LF_ANY_CPU
            return IntPtr.Size == 8 ? Native.RequestFloatingLicense_x64() : Native.RequestFloatingLicense();
#else
            return Native.RequestFloatingLicense();
#endif
        }

        /*
            FUNCTION: DropFloatingLicense()

            PURPOSE: Sends the request to the LexFloatServer to free the license.

            Call this function before you exit your application to prevent zombie licenses.

            RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE, LF_E_HOST_URL, LF_E_CALLBACK,
            LF_E_INET, LF_E_LICENSE_NOT_FOUND, LF_E_CLIENT, LF_E_IP, LF_E_SERVER,
            LF_E_SERVER_LICENSE_NOT_ACTIVATED, LF_E_SERVER_TIME_MODIFIED, LF_E_SERVER_LICENSE_SUSPENDED,
            LF_E_SERVER_LICENSE_GRACE_PERIOD_OVER, LF_E_SERVER_LICENSE_EXPIRED
        */
        public static int DropFloatingLicense()
        {
#if LF_ANY_CPU
            return IntPtr.Size == 8 ? Native.DropFloatingLicense_x64() : Native.DropFloatingLicense();
#else
            return Native.DropFloatingLicense();
#endif
        }

        /*
            FUNCTION: HasFloatingLicense()

            PURPOSE: Checks whether any license has been leased or not. If yes,
            it retuns LF_OK.

            RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE
        */
        public static int HasFloatingLicense()
        {
#if LF_ANY_CPU
            return IntPtr.Size == 8 ? Native.HasFloatingLicense_x64() : Native.HasFloatingLicense();
#else
            return Native.HasFloatingLicense();
#endif
        }

        /*
            FUNCTION: IncrementFloatingClientMeterAttributeUses()

            PURPOSE: Increments the meter attribute uses of the floating client.

            PARAMETERS:
            * name - name of the meter attribute
            * increment - the increment value

            RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE, LF_E_HOST_URL, LF_E_METER_ATTRIBUTE_NOT_FOUND,
            LF_E_INET, LF_E_LICENSE_NOT_FOUND, LF_E_CLIENT, LF_E_IP, LF_E_SERVER, LF_E_METER_ATTRIBUTE_USES_LIMIT_REACHED,
            LF_E_SERVER_LICENSE_NOT_ACTIVATED, LF_E_SERVER_TIME_MODIFIED, LF_E_SERVER_LICENSE_SUSPENDED,
            LF_E_SERVER_LICENSE_GRACE_PERIOD_OVER, LF_E_SERVER_LICENSE_EXPIRED
        */
        public static int IncrementFloatingClientMeterAttributeUses(string name, uint increment)
        {
#if LF_ANY_CPU 
            return IntPtr.Size == 8 ? Native.IncrementFloatingClientMeterAttributeUses_x64(name, increment) : Native.IncrementFloatingClientMeterAttributeUses(name, increment);
#else 
            return Native.IncrementFloatingClientMeterAttributeUses(name, increment);
#endif
        }

        /*
            FUNCTION: DecrementFloatingClientMeterAttributeUses()

            PURPOSE: Decrements the meter attribute uses of the floating client.

            PARAMETERS:
            * name - name of the meter attribute
            * decrement - the decrement value

            RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE, LF_E_HOST_URL, LF_E_METER_ATTRIBUTE_NOT_FOUND,
            LF_E_INET, LF_E_LICENSE_NOT_FOUND, LF_E_CLIENT, LF_E_IP, LF_E_SERVER,
            LF_E_SERVER_LICENSE_NOT_ACTIVATED, LF_E_SERVER_TIME_MODIFIED, LF_E_SERVER_LICENSE_SUSPENDED,
            LF_E_SERVER_LICENSE_GRACE_PERIOD_OVER, LF_E_SERVER_LICENSE_EXPIRED

            NOTE: If the decrement is more than the current uses, it resets the uses to 0.
        */
        public static int DecrementFloatingClientMeterAttributeUses(string name, uint decrement)
        {
#if LF_ANY_CPU 
            return IntPtr.Size == 8 ? Native.DecrementFloatingClientMeterAttributeUses_x64(name, decrement) : Native.DecrementFloatingClientMeterAttributeUses(name, decrement);
#else 
            return Native.DecrementFloatingClientMeterAttributeUses(name, decrement);
#endif
        }

        /*
            FUNCTION: ResetFloatingClientMeterAttributeUses()

            PURPOSE: Resets the meter attribute uses consumed by the floating client.

            PARAMETERS:
            * name - name of the meter attribute

            RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE, LF_E_HOST_URL, LF_E_METER_ATTRIBUTE_NOT_FOUND,
            LF_E_INET, LF_E_LICENSE_NOT_FOUND, LF_E_CLIENT, LF_E_IP, LF_E_SERVER,
            LF_E_SERVER_LICENSE_NOT_ACTIVATED, LF_E_SERVER_TIME_MODIFIED, LF_E_SERVER_LICENSE_SUSPENDED,
            LF_E_SERVER_LICENSE_GRACE_PERIOD_OVER, LF_E_SERVER_LICENSE_EXPIRED
        */
        public static int ResetFloatingClientMeterAttributeUses(string name)
        {
#if LF_ANY_CPU 
            return IntPtr.Size == 8 ? Native.ResetFloatingClientMeterAttributeUses_x64(name) : Native.ResetFloatingClientMeterAttributeUses(name);
#else 
            return Native.ResetFloatingClientMeterAttributeUses(name);
#endif
        }

        public static class StatusCodes
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

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CallbackType(uint status);

        /* To prevent garbage collection of delegate, need to keep a reference */
        static readonly List<CallbackType> callbackList = new List<CallbackType>();


        static class Native
        {
            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetHostProductId(string productId);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetHostUrl(string hostUrl);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetFloatingLicenseCallback(CallbackType callback);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetFloatingClientMetadata(string key, string value);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetHostLicenseMetadata(string key, StringBuilder value, int length);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetHostLicenseMeterAttribute(string name, ref uint allowedUses, ref uint totalUses);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetHostLicenseExpiryDate(ref uint expiryDate);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetFloatingClientMeterAttributeUses(string name, ref uint uses);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int RequestFloatingLicense();

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int DropFloatingLicense();

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int HasFloatingLicense();

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int IncrementFloatingClientMeterAttributeUses(string name, uint increment);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int DecrementFloatingClientMeterAttributeUses(string name, uint decrement);

            [DllImport(DLL_FILE_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            public static extern int ResetFloatingClientMeterAttributeUses(string name);

#if LF_ANY_CPU
            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetHostProductId", CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetHostProductId_x64(string productId);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetHostUrl", CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetHostUrl_x64(string hostUrl);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetFloatingLicenseCallback", CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetFloatingLicenseCallback_x64(CallbackType callback);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "SetFloatingClientMetadata", CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetFloatingClientMetadata_x64(string key, string value);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetHostLicenseMetadata", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetHostLicenseMetadata_x64(string key, StringBuilder value, int length);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetHostLicenseMeterAttribute", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetHostLicenseMeterAttribute_x64(string name, ref uint allowedUses, ref uint totalUses);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetHostLicenseExpiryDate", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetHostLicenseExpiryDate_x64(ref uint expiryDate);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "GetFloatingClientMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetFloatingClientMeterAttributeUses_x64(string name, ref uint uses);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "RequestFloatingLicense", CallingConvention = CallingConvention.Cdecl)]
            public static extern int RequestFloatingLicense_x64();

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "DropFloatingLicense", CallingConvention = CallingConvention.Cdecl)]
            public static extern int DropFloatingLicense_x64();

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "HasFloatingLicense", CallingConvention = CallingConvention.Cdecl)]
            public static extern int HasFloatingLicense_x64();

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "IncrementFloatingClientMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
            public static extern int IncrementFloatingClientMeterAttributeUses_x64(string name, uint increment);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "DecrementFloatingClientMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
            public static extern int DecrementFloatingClientMeterAttributeUses_x64(string name, uint decrement);

            [DllImport(DLL_FILE_NAME_X64, CharSet = CharSet.Unicode, EntryPoint = "ResetFloatingClientMeterAttributeUses", CallingConvention = CallingConvention.Cdecl)]
            public static extern int ResetFloatingClientMeterAttributeUses_x64(string name);
#endif
        }
    }
}