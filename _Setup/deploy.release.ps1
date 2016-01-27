param(
    [string]$msbuild = "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe",
    [string]$srcdir  = "C:\projects\geho",
    [string]$sln     = "$srcdir\Geho.sln",
    [string]$profile = "release",
    #[string]$inno    = "${env:ProgramFiles(x86)}\Inno Setup 5\Compil32.exe",
	[string]$inno    = "${env:ProgramFiles(x86)}\Inno Setup 5\iscc.exe",
    [string]$innoprj = "$srcdir\_Setup\Setup.iss"
)
<# ===========================================================
 # Definition of the functions
 =========================================================== #>

 function Build
 {
    Echo-Title "Building the solution"
	$cmdargs = @($sln, "/p:Configuration=$profile")
	$result = & $msbuild $cmdargs

    if (! $?) { throw "msbuild failed" }
 }

function BuildSetup  
{
	Echo-Title "Compiling Inno Setup project"
	& $inno $innoprj

    if (! $?) { throw "inno setup failed" }
	
}

function MoveFiles
{
    Echo-Title "Moving the files"
	mv geho.*.setup.exe "C:\Users\jibed_000\OneDrive\Public\Geho"
}

function Echo-Title([string] $message)
{
    Write-Host
    Write-Host "===========================================" -ForegroundColor Yellow
    Write-Host $message                                      -ForegroundColor Yellow
    Write-Host "===========================================" -ForegroundColor Yellow
    Write-Host
}

<# ===========================================================
 # Main entry point
 =========================================================== #>
#The script will stop on the first error
$ErrorActionPreference = "Stop"

Build
BuildSetup
MoveFiles