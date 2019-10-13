# TargetLogger

[![Build](https://github.com/joeltankam/msbuild-target-logger/workflows/build/badge.svg)](https://github.com/joeltankam/msbuild-target-logger/actions)
[![NuGet package](https://img.shields.io/nuget/v/TargetLogger.svg)](https://www.nuget.org/packages/TargetLogger/)

A console logger that logs only projects and targets being built.

## Requirements

- .NET Framework 4.6.1
- MSBuild 14.0

## Usage

`TargetLogger` can be downloaded from nuget : [packages/TargetLogger](https://www.nuget.org/packages/TargetLogger/)

```bash
nuget install TargetLogger -OutputDirectory packages
```

The logging assembly is located under : `lib\netstandard2.0\TargetLogger.dll`. This file is passed to MSBuild to specify our custom logging. 

```bash
msbuild /nologo /noconsolelogger /logger:packages/TargetLogger.1.0.0/lib/netstandar2.0/TargetLogger.dll
```

The `/noconsolelogger` switch is important to remove MSBuild's default console output.

## Sample

Here's an example using `Build.proj` from [`samples`](samples)

![Sample gif](./docs/sample.gif)
