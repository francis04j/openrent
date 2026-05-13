# demo-csharp-openrent

This repository contains a small .NET  C# solution for the OpenRent string-building exercise.

The library uses a result-type pattern for its public API, returning `Result<T>` values instead of using `null` or exceptions for expected validation failures.

Given an input string, the library builds a result by concatenating:

- the reversed input string
- the earliest alphabetic character found in the input
- `"open"` when the vowel count is odd, or `"rent"` when the vowel count is even

Example:

- Input: `nepo`
- Output: `openerent`

## Project Structure

- `src/OpenRentStringLib`: class library containing `OpenRentStringBuilder`
- `tests/OpenRentStringLib.Tests`: xUnit test project

## Prerequisites

- .NET 6 or higher SDK

Check your installed SDKs with:

```bash
dotnet --list-sdks
```

## Build From the Command Line

Restore dependencies and build the solution:

```bash
dotnet build demo-csharp-openrent.sln
```

## Run Tests From the Command Line

Execute the test project with:

```bash
dotnet test demo-csharp-openrent.sln
```

## Run the Application From the Command Line

This repository currently contains a class library and tests, but no standalone console application entry point such as a `Program.cs` executable.

The main behavior is exposed by `OpenRentStringBuilder.BuildResult(string input)` in `src/OpenRentStringLib/OpenRentStringBuilder.cs`.

If you want to exercise the implementation from the command line today, the supported workflow in this repository is:

```bash
dotnet build demo-csharp-openrent.sln
dotnet test demo-csharp-openrent.sln
```

If you want a true runnable CLI, add a small console app that references `OpenRentStringLib` and then run it with `dotnet run`.

## Library Usage Example

```csharp
using OpenRentStringLib;

var builder = new OpenRentStringBuilder();
var result = builder.BuildResult("nepo");

if (result.IsSuccess)
{
	Console.WriteLine(result.Value);
}
else
{
	Console.WriteLine($"{result.Error.Code}: {result.Error.Message}");
}
```