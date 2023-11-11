# _ZenKitCS_

[![Build](https://img.shields.io/github/actions/workflow/status/GothicKit/ZenKitCS/build.yml?label=Build&branch=main)](https://img.shields.io/github/actions/workflow/status/GothicKit/phoenix-shared-interface/build.yml)
![License](https://img.shields.io/github/license/GothicKit/ZenKitCS?label=License&color=important)
![C++](https://img.shields.io/static/v1?label=C%2B%2B&message=17&color=informational)
![Platforms](https://img.shields.io/static/v1?label=Supports&message=GCC%20|%20MinGW-w64%20|%20Clang%20|%20MSVC%20|%20Apple%20Clang&color=blueviolet)
![Version](https://img.shields.io/github/v/tag/GothicKit/ZenKitCS?label=Version&sort=semver)

A C#-library wrapping the [ZenKit](https://github.com/GothicKit/ZenKit) library for parsing game assets of
[PiranhaBytes'](https://www.piranha-bytes.com/) early 2000's games [Gothic](https://en.wikipedia.org/wiki/Gothic_(video_game))
and [Gothic II](https://en.wikipedia.org/wiki/Gothic_II).

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
