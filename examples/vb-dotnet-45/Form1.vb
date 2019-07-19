Imports FloatSample.Cryptlex

Public Class Form1

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub LicenceRenewCallback(ByVal status As UInteger)
        Select Case status
            Case LexFloatClient.StatusCodes.LF_OK
                Me.statusLabel.Text = "The license lease has renewed successfully."
                Exit Select
            Case LexFloatClient.StatusCodes.LF_E_LICENSE_NOT_FOUND
                Me.statusLabel.Text = "The license expired before it could be renewed."
                Exit Select
            Case LexFloatClient.StatusCodes.LF_E_LICENSE_EXPIRED_INET
                Me.statusLabel.Text = "The license expired due to network connection failure."
                Exit Select
            Case Else
                Me.statusLabel.Text = "The license renew failed due to other reason. Error code: " & status.ToString()
                Exit Select
        End Select
    End Sub

    Private Sub leaseBtn_Click(sender As Object, e As EventArgs) Handles leaseBtn.Click
        If LexFloatClient.HasFloatingLicense() = LexFloatClient.StatusCodes.LF_OK Then
            Return
        End If

        Dim status As Integer
        status = LexFloatClient.SetHostProductId("PASTE_YOUR_PRODUCT_ID")

        If status <> LexFloatClient.StatusCodes.LF_OK Then
            Me.statusLabel.Text = "Error setting product id: " & status.ToString()
            Return
        End If

        status = LexFloatClient.SetHostUrl("http://localhost:8090")

        If status <> LexFloatClient.StatusCodes.LF_OK Then
            Me.statusLabel.Text = "Error setting host url: " & status.ToString()
            Return
        End If

        status = LexFloatClient.SetFloatingLicenseCallback(AddressOf LicenceRenewCallback)

        If status <> LexFloatClient.StatusCodes.LF_OK Then
            Me.statusLabel.Text = "Error setting callback function: " & status.ToString()
            Return
        End If

        status = LexFloatClient.RequestFloatingLicense()

        If status <> LexFloatClient.StatusCodes.LF_OK Then
            Me.statusLabel.Text = "Error requesting license: " & status.ToString()
            Return
        End If

        Me.statusLabel.Text = "License leased successfully!"
    End Sub

    Private Sub dropBtn_Click(sender As Object, e As EventArgs) Handles dropBtn.Click
        If LexFloatClient.HasFloatingLicense() <> LexFloatClient.StatusCodes.LF_OK Then
            Return
        End If

        Dim status As Integer
        status = LexFloatClient.DropFloatingLicense()

        If status <> LexFloatClient.StatusCodes.LF_OK Then
            Me.statusLabel.Text = "Error dropping license: " & status.ToString()
            Return
        End If

        Me.statusLabel.Text = "License dropped successfully!"
    End Sub
End Class
