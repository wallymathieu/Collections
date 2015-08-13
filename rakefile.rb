require 'albacore'
require 'nuget_helper'

$dir = File.dirname(__FILE__)
$sln = File.join($dir, "WhatIsUpdated.sln")

desc "Install missing NuGet packages."
task :restore do
  NugetHelper.exec("restore #{$sln}")
end

desc "build"
build :build => [:restore] do |msb|
  msb.prop :configuration, :Debug
  msb.prop :platform, "Any CPU"
  msb.target = :Rebuild
  msb.be_quiet
  msb.nologo
  msb.sln = $sln 
end

task :default => ['build']

desc "test using nunit console"
test_runner :test do |nunit|
  nunit.exe = NugetHelper.nunit_path
  files = Dir.glob(File.join($dir,"**","bin","**","*Tests.dll")) 
  nunit.files = files 
end
