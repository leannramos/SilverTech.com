param(
  [String]
  $Url = "https://channel9-sample-app.azurewebsites.net",

  [String]
  $OutputDir = "./Output",

  [String]
  $EventName = "LighthouseResult",

  [bool]
  $EnableLog = $false
)

pushd
cd $PSScriptRoot

$AccessToken = 'Get auth token if needed'

Write-Host "Installing dependencies.."
Write-Host "--------------------------------"
npm install

Write-Host "Running node script.."
Write-Host "--------------------------------"
node lighthouse.js $Url $AccessToken $OutputDir $EnableLog $EventName
exit $LASTEXITCODE


