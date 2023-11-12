# _ZenKitCS_

[![Build](https://img.shields.io/github/actions/workflow/status/GothicKit/ZenKitCS/build.yml?label=Build&branch=main)](https://img.shields.io/github/actions/workflow/status/GothicKit/phoenix-shared-interface/build.yml)
![License](https://img.shields.io/github/license/GothicKit/ZenKitCS?label=License&color=important)
![C#](https://img.shields.io/static/v1?label=C%23&message=netstandard2.1&color=informational)
![Platforms](https://img.shields.io/static/v1?label=Supports&message=Linux%20x64%20|%20Windows%20x64%20|%20Android%20ARM64&color=blueviolet)
![Version](https://img.shields.io/github/v/tag/GothicKit/ZenKitCS?label=Version&sort=semver)

A C#-library wrapping the [ZenKit](https://github.com/GothicKit/ZenKit) library for parsing game assets of
[PiranhaBytes'](https://www.piranha-bytes.com/) early 2000's games [Gothic](https://en.wikipedia.org/wiki/Gothic_(video_game))
and [Gothic II](https://en.wikipedia.org/wiki/Gothic_II).

## Using

You can install `ZenKitCS` from the [NuGet Package Gallery](https://www.nuget.org/packages/ZenKit). Simply add the
following snippet to your `.csproj` file, replacing the version with the approprite version identifier from NuGet.

```
<ItemGroup>
  <PackageReference Include="ZenKit" Version="x.x.x" />
</ItemGroup>
```

To build your project then, you will need to add a `RuntimeIdentifiers` property to your `.csproj`. You can simply
use this once and copy it the topmost `<PropertyGroup>` in your `.csproj` like this:

```xml
<PropertyGroup>
    <!-- ... -->
    
    <RuntimeIdentifiers>linux-x64;win-x64;osx-x64;android-arm64</RuntimeIdentifiers>

    <!-- ... -->
</PropertyGroup>
```

You can now also build your project for those runtimes by supplying the runtime identifier in `dotnet build` using the
`-r` parameter. This is how you would build your project for Android:

```
dotnet build -r android-arm64 -c Release --self-contained
```

## Building

You will need:

* .NET Standard 2.1 (for `ZenKit` itself) and .NET 7 (for `ZenKit.Tests`), anything onward should work as well
* Git

To build _ZenKitCS_ from scratch, just open a terminal in a directory of your choice and run

```bash
git clone --recursive https://github.com/GothicKit/ZenKitCS
cd ZenKitCS
dotnet build
```

You will find the built library in `ZenKit/bin/Debug/netstandard2.1`.

## Testing

To test _ZenKitCS_, just run `dotnet test` in the Git folder or in the `ZenKit.Test` project.
