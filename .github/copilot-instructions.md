# Copilot Instructions for Figgle

## Build and Test

```shell
# Build entire solution
dotnet build Figgle.slnx

# Run all tests
dotnet test Figgle.slnx

# Run a single test project
dotnet test tests/Figgle.Tests/Figgle.Tests.csproj

# Run a single test by name
dotnet test tests/Figgle.Tests/Figgle.Tests.csproj --filter "FullyQualifiedName~FiggleFontTest.Render"
```

## Architecture

Figgle is an ASCII banner generator for .NET, implementing the [FIGlet](http://www.figlet.org/) specification. It ships as three NuGet packages, built from four projects plus an internal source generator:

- **`Figgle`** (`src/Figgle/`) — Core library. Contains `FiggleFontParser` (parses `.flf` FIGlet font files) and `FiggleFont` (renders text using parsed fonts, implementing smushing/kerning rules from the FIGlet spec). Targets `netstandard2.0`.
- **`Figgle.Fonts`** (`src/Figgle.Fonts/`) — Collection of 250+ bundled FIGlet fonts exposed via `FiggleFonts` static properties. Fonts from `fonts/*.flf` are zipped at build time into an embedded resource (`Fonts.zip`) and lazily loaded on first access.
- **`Figgle.Generator`** (`src/Figgle.Generator/`) — Public-facing Roslyn incremental source generator shipped as a NuGet package. Provides two generators:
  - `RenderTextSourceGenerator` — renders static text at compile time via `[RenderFiggleText]` attribute.
  - `EmbedFontSourceGenerator` — embeds a font into the consuming assembly via `[EmbedFiggleFont]` attribute.
- **`Figgle.Fonts.Generator`** (`src/Figgle.Fonts.Generator/`) — Internal source generator (not published as a NuGet package). Auto-generates the `FiggleFonts` class with a property per font, including XML doc comments with rendered sample text.

### Font name mapping

`src/Figgle.Fonts/Aliases.csv` maps font file names to C# property names on `FiggleFonts` (e.g., `3-d,ThreeD`). Fonts not listed in this file get an auto-generated PascalCase property name. When adding a new font, add it to `fonts/` and optionally add an alias row if the auto-generated name is unclear.

### Test projects

- **`Figgle.Tests`** — Unit tests for the core library (font parsing, rendering, string pool).
- **`Figgle.Generator.Tests`** — Unit tests for source generators using in-memory Roslyn compilation.
- **`Figgle.Generator.AcceptanceTests`** — End-to-end tests that exercise the source generators as an actual project reference (simulating NuGet package consumption).

## Key Conventions

- **File header** — Every `.cs` file must start with: `// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.` (enforced by IDE0073).
- **Private fields** — Prefix with `_` (e.g., `_pool`). Private static fields also use `_` prefix.
- **Central Package Management** — Package versions are in `Directory.Packages.props` at the repo root. Individual projects reference packages without specifying versions.
- **Testing** — xUnit with `[Fact]` and `[Theory]` attributes. Global `using Xunit;` is set in `tests/Directory.Build.props`.
