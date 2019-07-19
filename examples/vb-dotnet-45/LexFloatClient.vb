
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Collections.Generic

Namespace Cryptlex
    Public NotInheritable Class LexFloatClient

        Private Sub New()
        End Sub

        Private Const DLL_FILE_NAME As String = "LexFloatClient.dll"

        '
        '     In order to use "Any CPU" configuration, rename 64 bit LexFloatClient.dll to LexFloatClient64.dll
        '     and uncomment "LF_ANY_CPU" conditional compiler constant.
        '
        '
        '#Const LF_ANY_CPU = 1

#If LF_ANY_CPU Then
        Private Const DLL_FILE_NAME_X64 As String = "LexFloatClient64.dll"
#End If


        '
        '     FUNCTION: SetHostProductId()

        '     PURPOSE: Sets the product id of your application.

        '     This function must be called on every start of your program before
        '     any other functions are called, with the exception of SetProductFile()
        '     or SetProductData() function.

        '     PARAMETERS:
        '     * productId - the unique product id of your application as mentioned
        '     on the product page in the dashboard.

        '     RETURN CODES: LF_OK, LF_E_PRODUCT_ID
        '
        Public Shared Function SetHostProductId(productId As String) As Integer
#If LF_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.SetHostProductId_x64(productId), Native.SetHostProductId(productId))
#Else
            Return Native.SetHostProductId(productId)
#End If
        End Function

        '
        '     FUNCTION: SetHostUrl()

        '     PURPOSE: Sets the network address of the LexFloatServer.

        '     The url format should be: http://[ip or hostname]:[port]

        '     PARAMETERS:
        '     * hostUrl - url string having the correct format

        '     RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_HOST_URL
        '
        Public Shared Function SetHostUrl(hostUrl As String) As Integer
#If LF_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.SetHostUrl_x64(hostUrl), Native.SetHostUrl(hostUrl))
#Else
            Return Native.SetHostUrl(hostUrl)
#End If
        End Function

        '
        '     FUNCTION: SetFloatingLicenseCallback()

        '     PURPOSE: Sets the renew license callback function.

        '     Whenever the license lease is about to expire, a renew request is sent to the
        '     server. When the request completes, the license callback function
        '     gets invoked with one of the following status codes:

        '     LF_OK, LF_E_INET, LF_E_LICENSE_EXPIRED_INET, LF_E_LICENSE_NOT_FOUND, LF_E_CLIENT, LF_E_IP,
        '     LF_E_SERVER, LF_E_TIME, LF_E_SERVER_LICENSE_NOT_ACTIVATED,LF_E_SERVER_TIME_MODIFIED,
        '     LF_E_SERVER_LICENSE_SUSPENDED, LF_E_SERVER_LICENSE_EXPIRED, LF_E_SERVER_LICENSE_GRACE_PERIOD_OVER

        '     PARAMETERS:
        '     * callback - name of the callback function

        '     RETURN CODES: LF_OK, LF_E_PRODUCT_ID
        '
        Public Shared Function SetFloatingLicenseCallback(callback As CallbackType) As Integer
            Dim wrappedCallback = callback
            Dim syncTarget = TryCast(callback.Target, System.Windows.Forms.Control)
            If syncTarget IsNot Nothing Then
                wrappedCallback = Function(v) syncTarget.Invoke(callback, New Object() {v})
            End If
            callbackList.Add(wrappedCallback)
#If LF_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.SetFloatingLicenseCallback_x64(wrappedCallback), Native.SetFloatingLicenseCallback(wrappedCallback))
#Else
            Return Native.SetFloatingLicenseCallback(wrappedCallback)
#End If
        End Function

        '
        '     FUNCTION: SetFloatingClientMetadata()

        '     PURPOSE: Sets the floating client metadata.

        '     The  metadata appears along with the license details of the license
        '     in LexFloatServer dashboard.

        '     PARAMETERS:
        '     * key - key of the metadata field whose value you want to get
        '     * value - pointer to a buffer that receives the value of the string
        '     * length - size of the buffer pointed to by the value parameter

        '     RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE, LF_E_BUFFER_SIZE,
        '     LF_E_METADATA_KEY_NOT_FOUND
        '
        Public Shared Function SetFloatingClientMetadata(key As String, value As String) As Integer
#If LF_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.SetFloatingClientMetadata_x64(key, value), Native.SetFloatingClientMetadata(key, value))
#Else
            Return Native.SetFloatingClientMetadata(key, value)
#End If
        End Function

        '
        '     FUNCTION: GetHostLicenseMetadata()

        '     PURPOSE: Get the value of the license metadata field associated with the LexFloatServer license.

        '     PARAMETERS:
        '     * key - key of the metadata field whose value you want to get
        '     * value - pointer to a buffer that receives the value of the string
        '     * length - size of the buffer pointed to by the value parameter

        '     RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE, LF_E_BUFFER_SIZE,
        '     LF_E_METADATA_KEY_NOT_FOUND
        '
        Public Shared Function GetHostLicenseMetadata(key As String, value As StringBuilder, length As Integer) As Integer
#If LF_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetHostLicenseMetadata_x64(key, value, length), Native.GetHostLicenseMetadata(key, value, length))
#Else
            Return Native.GetHostLicenseMetadata(key, value, length)
#End If
        End Function

        '
        '     FUNCTION: GetHostLicenseMeterAttribute()

        '     PURPOSE: Gets the license meter attribute allowed uses and total uses associated with the LexFloatServer license.

        '     PARAMETERS:
        '     * name - name of the meter attribute
        '     * allowedUses - pointer to the integer that receives the value
        '     * totalUses - pointer to the integer that receives the value

        '     RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE, LF_E_METER_ATTRIBUTE_NOT_FOUND
        '
        Public Shared Function GetHostLicenseMeterAttribute(ByVal name As String, ByRef allowedUses As UInteger, ByRef totalUses As UInteger) As Integer
#If LF_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetHostLicenseMeterAttribute_x64(name, allowedUses, totalUses), Native.GetHostLicenseMeterAttribute(name, allowedUses, totalUses))
#Else
            Return Native.GetHostLicenseMeterAttribute(name, allowedUses, totalUses)
#End If
        End Function

        '
        '     FUNCTION: GetHostLicenseExpiryDate()

        '     PURPOSE: Gets the license expiry date timestamp of the LexFloatServer license.

        '     PARAMETERS:
        '     * expiryDate - pointer to the integer that receives the value

        '     RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE
        ' 
        Public Shared Function GetHostLicenseExpiryDate(ByRef expiryDate As UInteger) As Integer
#If LF_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetHostLicenseExpiryDate_x64(expiryDate), Native.GetHostLicenseExpiryDate(expiryDate))
#Else
            Return Native.GetHostLicenseExpiryDate(expiryDate)
#End If
        End Function

        '
        '     FUNCTION: GetFloatingClientMeterAttributeUses()

        '     PURPOSE: Gets the meter attribute uses consumed by the floating client.

        '     PARAMETERS:
        '     * name - name of the meter attribute
        '     * uses - pointer to the integer that receives the value

        '     RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE, LF_E_METER_ATTRIBUTE_NOT_FOUND
        '
        Public Shared Function GetFloatingClientMeterAttributeUses(ByVal name As String, ByRef uses As UInteger) As Integer
#If LF_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.GetFloatingClientMeterAttributeUses_x64(name, uses), Native.GetFloatingClientMeterAttributeUses(name, uses))
#Else
            Return Native.GetFloatingClientMeterAttributeUses(name, uses)
#End If
        End Function

        '
        '     FUNCTION: RequestFloatingLicense()

        '     PURPOSE: Sends the request to lease the license from the LexFloatServer.

        '     RETURN CODES: LF_OK, LF_FAIL, LF_E_PRODUCT_ID, LF_E_LICENSE_EXISTS, LF_E_HOST_URL,
        '     LF_E_CALLBACK, LF_E_LICENSE_LIMIT_REACHED, LF_E_INET, LF_E_TIME, LF_E_CLIENT, LF_E_IP, LF_E_SERVER,
        '     LF_E_SERVER_LICENSE_NOT_ACTIVATED, LF_E_SERVER_TIME_MODIFIED, LF_E_SERVER_LICENSE_SUSPENDED,
        '     LF_E_SERVER_LICENSE_GRACE_PERIOD_OVER, LF_E_SERVER_LICENSE_EXPIRED
        '  
        Public Shared Function RequestFloatingLicense() As Integer
#If LF_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.RequestFloatingLicense_x64(), Native.RequestFloatingLicense())
#Else
            Return Native.RequestFloatingLicense()
#End If
        End Function

        '
        '     FUNCTION: DropFloatingLicense()

        '     PURPOSE: Sends the request to the LexFloatServer to free the license.

        '     Call this function before you exit your application to prevent zombie licenses.

        '     RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE, LF_E_HOST_URL, LF_E_CALLBACK,
        '     LF_E_INET, LF_E_LICENSE_NOT_FOUND, LF_E_CLIENT, LF_E_IP, LF_E_SERVER,
        '     LF_E_SERVER_LICENSE_NOT_ACTIVATED, LF_E_SERVER_TIME_MODIFIED, LF_E_SERVER_LICENSE_SUSPENDED,
        '     LF_E_SERVER_LICENSE_GRACE_PERIOD_OVER, LF_E_SERVER_LICENSE_EXPIRED
        '  
        Public Shared Function DropFloatingLicense() As Integer
#If LF_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.DropFloatingLicense_x64(), Native.DropFloatingLicense())
#Else
            Return Native.DropFloatingLicense()
#End If
        End Function

        '
        '     FUNCTION: HasFloatingLicense()

        '     PURPOSE: Checks whether any license has been leased or not. If yes,
        '     it retuns LF_OK.

        '     RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE
        '   
        Public Shared Function HasFloatingLicense() As Integer
#If LF_ANY_CPU Then
            'Return If(IntPtr.Size = 8, Native.HasFloatingLicense_x64(), Native.HasFloatingLicense())
#Else
            Return Native.HasFloatingLicense()
#End If
        End Function

        '
        '     FUNCTION: IncrementFloatingClientMeterAttributeUses()

        '     PURPOSE: Increments the meter attribute uses of the floating client.

        '     PARAMETERS:
        '     * name - name of the meter attribute
        '     * increment - the increment value

        '     RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE, LF_E_HOST_URL, LF_E_METER_ATTRIBUTE_NOT_FOUND,
        '     LF_E_INET, LF_E_LICENSE_NOT_FOUND, LF_E_CLIENT, LF_E_IP, LF_E_SERVER, LF_E_METER_ATTRIBUTE_USES_LIMIT_REACHED,
        '     LF_E_SERVER_LICENSE_NOT_ACTIVATED, LF_E_SERVER_TIME_MODIFIED, LF_E_SERVER_LICENSE_SUSPENDED,
        '     LF_E_SERVER_LICENSE_GRACE_PERIOD_OVER, LF_E_SERVER_LICENSE_EXPIRED

        '
        Public Shared Function IncrementFloatingClientMeterAttributeUses(ByVal name As String, increment As UInteger) As Integer
#If LF_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.IncrementFloatingClientMeterAttributeUses_x64(name, increment), Native.IncrementFloatingClientMeterAttributeUses(name, increment))
#Else
            Return Native.IncrementFloatingClientMeterAttributeUses(name, increment)
#End If
        End Function

        '
        '     FUNCTION: DecrementFloatingClientMeterAttributeUses()

        '     PURPOSE: Decrements the meter attribute uses of the floating client.

        '     PARAMETERS:
        '     * name - name of the meter attribute
        '     * decrement - the decrement value

        '     RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE, LF_E_HOST_URL, LF_E_METER_ATTRIBUTE_NOT_FOUND,
        '     LF_E_INET, LF_E_LICENSE_NOT_FOUND, LF_E_CLIENT, LF_E_IP, LF_E_SERVER,
        '     LF_E_SERVER_LICENSE_NOT_ACTIVATED, LF_E_SERVER_TIME_MODIFIED, LF_E_SERVER_LICENSE_SUSPENDED,
        '     LF_E_SERVER_LICENSE_GRACE_PERIOD_OVER, LF_E_SERVER_LICENSE_EXPIRED

        '
        Public Shared Function DecrementFloatingClientMeterAttributeUses(ByVal name As String, decrement As UInteger) As Integer
#If LF_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.DecrementFloatingClientMeterAttributeUses_x64(name, decrement), Native.DecrementFloatingClientMeterAttributeUses(name, decrement))
#Else
            Return Native.DecrementFloatingClientMeterAttributeUses(name, decrement)
#End If
        End Function

        '
        '     FUNCTION: ResetFloatingClientMeterAttributeUses()

        '     PURPOSE: Resets the meter attribute uses consumed by the floating client.

        '     PARAMETERS:
        '     * name - name of the meter attribute

        '     RETURN CODES: LF_OK, LF_E_PRODUCT_ID, LF_E_NO_LICENSE, LF_E_HOST_URL, LF_E_METER_ATTRIBUTE_NOT_FOUND,
        '     LF_E_INET, LF_E_LICENSE_NOT_FOUND, LF_E_CLIENT, LF_E_IP, LF_E_SERVER,
        '     LF_E_SERVER_LICENSE_NOT_ACTIVATED, LF_E_SERVER_TIME_MODIFIED, LF_E_SERVER_LICENSE_SUSPENDED,
        '     LF_E_SERVER_LICENSE_GRACE_PERIOD_OVER, LF_E_SERVER_LICENSE_EXPIRED

        '
        Public Shared Function ResetFloatingClientMeterAttributeUses(ByVal name As String) As Integer
#If LF_ANY_CPU Then
            Return If(IntPtr.Size = 8, Native.ResetFloatingClientMeterAttributeUses_x64(name), Native.ResetFloatingClientMeterAttributeUses(name))
#Else
            Return Native.ResetFloatingClientMeterAttributeUses(name)
#End If
        End Function

        Public Enum StatusCodes As UInteger

            '
            '    CODE: LF_OK

            '    MESSAGE: Success code.
            '
            LF_OK = 0

            '
            '    CODE: LF_FAIL

            '    MESSAGE: Failure code.
            '
            LF_FAIL = 1

            '
            '    CODE: LF_E_PRODUCT_ID

            '    MESSAGE: The product id is incorrect.
            '
            LF_E_PRODUCT_ID = 40

            '
            '    CODE: LF_E_CALLBACK

            '    MESSAGE: Invalid or missing callback function.
            '
            LF_E_CALLBACK = 41

            '
            '    CODE: LF_E_HOST_URL

            '    MESSAGE: Missing or invalid server url.
            '
            LF_E_HOST_URL = 42

            '
            '    CODE: LF_E_TIME

            '    MESSAGE: Ensure system date and time settings are correct.
            '
            LF_E_TIME = 43

            '
            '    CODE: LF_E_INET

            '    MESSAGE: Failed to connect to the server due to network error.
            '
            LF_E_INET = 44

            '
            '    CODE: LF_E_NO_LICENSE

            '    MESSAGE: License has not been leased yet.
            '
            LF_E_NO_LICENSE = 45

            '
            '    CODE: LF_E_LICENSE_EXISTS

            '    MESSAGE: License has already been leased.
            '
            LF_E_LICENSE_EXISTS = 46

            '
            '    CODE: LF_E_LICENSE_NOT_FOUND

            '    MESSAGE: License does not exist on server or has already expired. This
            '    happens when the request to refresh the license is delayed.
            '
            LF_E_LICENSE_NOT_FOUND = 47

            '
            '    CODE: LF_E_LICENSE_EXPIRED_INET

            '    MESSAGE: License lease has expired due to network error. This
            '    happens when the request to refresh the license fails due to
            '    network error.
            '
            LF_E_LICENSE_EXPIRED_INET = 48

            '
            '    CODE: LF_E_LICENSE_LIMIT_REACHED

            '    MESSAGE: The server has reached it's allowed limit of floating licenses.
            '
            LF_E_LICENSE_LIMIT_REACHED = 49

            '
            '    CODE: LF_E_BUFFER_SIZE

            '    MESSAGE: The buffer size was smaller than required.
            '
            LF_E_BUFFER_SIZE = 50

            '
            '    CODE: LF_E_METADATA_KEY_NOT_FOUND

            '    MESSAGE: The metadata key does not exist.
            '
            LF_E_METADATA_KEY_NOT_FOUND = 51

            '
            '    CODE: LF_E_METADATA_KEY_LENGTH

            '    MESSAGE: Metadata key length is more than 256 characters.
            '
            LF_E_METADATA_KEY_LENGTH = 52

            '
            '    CODE: LF_E_METADATA_VALUE_LENGTH

            '    MESSAGE: Metadata value length is more than 256 characters.
            '
            LF_E_METADATA_VALUE_LENGTH = 53

            '
            '    CODE: LF_E_ACTIVATION_METADATA_LIMIT

            '    MESSAGE: The floating client has reached it's metadata fields limit.
            '
            LF_E_FLOATING_CLIENT_METADATA_LIMIT = 54

            '
            '    CODE: LF_E_METER_ATTRIBUTE_NOT_FOUND

            '    MESSAGE: The meter attribute does not exist.
            '
            LF_E_METER_ATTRIBUTE_NOT_FOUND = 55

            '
            '    CODE: LF_E_METER_ATTRIBUTE_USES_LIMIT_REACHED

            '    MESSAGE: The meter attribute has reached it's usage limit.
            '
            LF_E_METER_ATTRIBUTE_USES_LIMIT_REACHED = 56

            '
            '    CODE: LF_E_IP

            '    MESSAGE: IP address is not allowed.
            '
            LF_E_IP = 60

            '
            '    CODE: LF_E_CLIENT

            '    MESSAGE: Client error.
            '
            LF_E_CLIENT = 70

            '
            '    CODE: LF_E_SERVER

            '    MESSAGE: Server error.
            '
            LF_E_SERVER = 71

            '
            '    CODE: LF_E_SERVER_TIME_MODIFIED

            '    MESSAGE: System time on server has been tampered with. Ensure
            '    your date and time settings are correct on the server machine.
            '
            LF_E_SERVER_TIME_MODIFIED = 72

            '
            '    CODE: LF_E_SERVER_LICENSE_NOT_ACTIVATED

            '    MESSAGE: The server has not been activated using a license key.
            '
            LF_E_SERVER_LICENSE_NOT_ACTIVATED = 73

            '
            '    CODE: LF_E_SERVER_LICENSE_EXPIRED

            '    MESSAGE: The server license has expired.
            '
            LF_E_SERVER_LICENSE_EXPIRED = 74

            '
            '    CODE: LF_E_SERVER_LICENSE_SUSPENDED

            '    MESSAGE: The server license has been suspended.
            '
            LF_E_SERVER_LICENSE_SUSPENDED = 75

            '
            '    CODE: LF_E_SERVER_LICENSE_GRACE_PERIOD_OVER

            '    MESSAGE: The grace period for server license is over.
            '
            LF_E_SERVER_LICENSE_GRACE_PERIOD_OVER = 76

        End Enum

        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub CallbackType(status As UInteger)

        ' To prevent garbage collection of delegate, need to keep a reference 
        Shared ReadOnly callbackList As List(Of CallbackType) = New List(Of CallbackType)()

        Private NotInheritable Class Native
            Private Sub New()
            End Sub

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetHostProductId(ByVal productId As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetHostUrl(ByVal hostUrl As String) As Integer
            End Function
            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetFloatingLicenseCallback(ByVal callback As CallbackType) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetFloatingClientMetadata(ByVal key As String, ByVal value As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetHostLicenseMetadata(ByVal key As String, ByVal value As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetHostLicenseMeterAttribute(ByVal name As String, ByRef allowedUses As UInteger, ByRef totalUses As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetHostLicenseExpiryDate(ByRef expiryDate As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetFloatingClientMeterAttributeUses(ByVal name As String, ByRef uses As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function RequestFloatingLicense() As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function DropFloatingLicense() As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function HasFloatingLicense() As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function IncrementFloatingClientMeterAttributeUses(ByVal name As String, increment As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function DecrementFloatingClientMeterAttributeUses(ByVal name As String, decrement As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function ResetFloatingClientMeterAttributeUses(ByVal name As String) As Integer
            End Function

#If LF_ANY_CPU Then

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="SetHostProductId", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetHostProductId_x64(ByVal productId As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="SetHostUrl", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetHostUrl_x64(ByVal hostUrl As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="SetFloatingLicenseCallback", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetFloatingLicenseCallback_x64(ByVal callback As CallbackType) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="SetFloatingClientMetadata", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function SetFloatingClientMetadata_x64(ByVal key As String, ByVal value As String) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetHostLicenseMetadata", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetHostLicenseMetadata_x64(ByVal key As String, ByVal value As StringBuilder, ByVal length As Integer) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetHostLicenseMeterAttribute", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetHostLicenseMeterAttribute_x64(ByVal name As String, ByRef allowedUses As UInteger, ByRef totalUses As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetHostLicenseExpiryDate", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetHostLicenseExpiryDate_x64(ByRef expiryDate As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="GetFloatingClientMeterAttributeUses", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function GetFloatingClientMeterAttributeUses_x64(ByVal name As String, ByRef uses As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="RequestFloatingLicense", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function RequestFloatingLicense_x64() As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="DropFloatingLicense", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function DropFloatingLicense_x64() As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="HasFloatingLicense", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function HasFloatingLicense_x64() As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="IncrementFloatingClientMeterAttributeUses", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function IncrementFloatingClientMeterAttributeUses_x64(ByVal name As String, increment As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="DecrementFloatingClientMeterAttributeUses", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function DecrementFloatingClientMeterAttributeUses_x64(ByVal name As String, decrement As UInteger) As Integer
            End Function

            <DllImport(DLL_FILE_NAME_X64, CharSet:=CharSet.Unicode, EntryPoint:="ResetFloatingClientMeterAttributeUses", CallingConvention:=CallingConvention.Cdecl)>
            Public Shared Function ResetFloatingClientMeterAttributeUses_x64(ByVal name As String) As Integer
            End Function
#End If
        End Class
    End Class
End Namespace
