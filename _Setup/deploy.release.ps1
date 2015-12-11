param(
    [string]$msbuild = "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe",
    [string]$srcdir  = "C:\projects\geho",
    [string]$sln     = "$srcdir\Geho.sln",
    [string]$profile = "release",
    #[string]$inno    = "${env:ProgramFiles(x86)}\Inno Setup 5\Compil32.exe",
	[string]$inno    = "${env:ProgramFiles(x86)}\Inno Setup 5\iscc.exe",
    [string]$innoprj = "$srcdir\_Setup\Setup.iss"
)
<#
 # Building solution
 #>
 $cmdargs = @($sln, "/p:Configuration=$profile")
 & $msbuild $cmdargs

 <#
  # Compiling the inno setup file
  #>
  echo "Compiling Inno Setup project"
  & $inno $innoprj
  
  mv geho.*.setup.exe "C:\Users\jibed_000\OneDrive\Public\Geho"