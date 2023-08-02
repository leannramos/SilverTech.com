param(
  [String]
  $Url = "https://silvertech-com-staging.aws.silvertech.net/",

  [String]
  $OutputDir = "./Output"
)

pushd
cd $PSScriptRoot

$AccessToken = 'Get auth token if needed'

Write-Host "Installing dependencies.."
Write-Host "--------------------------------"
npm install

Write-Host "Running node script.."
Write-Host "--------------------------------"
node axe.js $Url $AccessToken $OutputDir
exit $LASTEXITCODE