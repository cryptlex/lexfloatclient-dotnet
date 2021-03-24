$base_url = "https://dl.cryptlex.com/downloads"
$lexfloatclient_version ="v4.5.0"
new-item -Name tmp -ItemType directory

$url = "$base_url/$lexfloatclient_version/LexFloatClient-Win.zip"
$output = "$PSScriptRoot\tmp\LexFloatClient-Win.zip"
(New-Object System.Net.WebClient).DownloadFile($url, $output)
Expand-Archive "$PSScriptRoot\tmp\LexFloatClient-Win.zip" -DestinationPath "$PSScriptRoot\tmp\LexFloatClient-Win"
Copy-Item -Path "$PSScriptRoot\tmp\LexFloatClient-Win\libs\vc14\x64\LexFloatClient.dll" -Destination "$PSScriptRoot\src\Cryptlex.LexFloatClient\runtimes\win-x64\native\LexFloatClient.dll"
Copy-Item -Path "$PSScriptRoot\tmp\LexFloatClient-Win\libs\vc14\x86\LexFloatClient.dll" -Destination "$PSScriptRoot\src\Cryptlex.LexFloatClient\runtimes\win-x86\native\LexFloatClient32.dll"

$url = "$base_url/$lexfloatclient_version/LexFloatClient-Linux.zip"
$output = "$PSScriptRoot\tmp\LexFloatClient-Linux.zip"
(New-Object System.Net.WebClient).DownloadFile($url, $output)
Expand-Archive "$PSScriptRoot\tmp\LexFloatClient-Linux.zip" -DestinationPath "$PSScriptRoot\tmp\LexFloatClient-Linux"
Copy-Item -Path "$PSScriptRoot\tmp\LexFloatClient-Linux\libs\gcc\amd64\libLexFloatClient.so" -Destination "$PSScriptRoot\src\Cryptlex.LexFloatClient\runtimes\linux-x64\native\libLexFloatClient.so"
Copy-Item -Path "$PSScriptRoot\tmp\LexFloatClient-Linux\libs\musl\amd64\libLexFloatClient.so" -Destination "$PSScriptRoot\src\Cryptlex.LexFloatClient\runtimes\linux-musl-x64\native\libLexFloatClient.so"

$url = "$base_url/$lexfloatclient_version/LexFloatClient-Mac.zip"
$output = "$PSScriptRoot\tmp\LexFloatClient-Mac.zip"
(New-Object System.Net.WebClient).DownloadFile($url, $output)
Expand-Archive "$PSScriptRoot\tmp\LexFloatClient-Mac.zip" -DestinationPath "$PSScriptRoot\tmp\LexFloatClient-Mac"
Copy-Item -Path "$PSScriptRoot\tmp\LexFloatClient-Mac\libs\clang\x86_64\libLexFloatClient.dylib" -Destination "$PSScriptRoot\src\Cryptlex.LexFloatClient\runtimes\osx-x64\native\libLexFloatClient.dylib"

