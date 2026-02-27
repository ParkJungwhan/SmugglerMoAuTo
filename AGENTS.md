# Repository Guidelines

## Project Structure & Module Organization
- `Src/MainApp/`: main WPF desktop client (`MainApp.csproj`, XAML views, app bootstrap).
- `SmugglerLib/`: shared libraries and third-party integrations consumed by `MainApp`.
- `SmugglerLib/SmugCommon`, `SmugControl`, `SmugOpenSource`, `SmugLinux`, `ThirdParty/*`: reusable modules grouped by concern.
- `SmugglerLib/Test/`: executable test harnesses (`TestConsole`, `TestCommonConsole`, `TestAPI`, `TestWindow`) used for validation and demos.
- `SmugglerMoAuTo.slnx`: top-level solution entry point for app + core libs.

## Build, Test, and Development Commands
- `dotnet restore SmugglerMoAuTo.slnx`: restore NuGet packages for the root solution.
- `dotnet build SmugglerMoAuTo.slnx -c Debug`: compile the app and referenced library projects.
- `dotnet run --project Src/MainApp/MainApp.csproj`: run the WPF main application locally.
- `dotnet build SmugglerLib/SmugglerLib.sln -c Debug`: build all shared-library and harness projects.
- `dotnet run --project SmugglerLib/Test/TestConsole/TestConsole.csproj` (or other `SmugglerLib/Test/*` project): run scenario-based checks.

## Coding Style & Naming Conventions
- Use 4-space indentation and UTF-8 text files.
- Follow existing C# conventions: `PascalCase` for types/methods/properties, `camelCase` for locals/parameters, `_camelCase` for private fields.
- Keep nullable reference warnings clean (`<Nullable>enable</Nullable>` is on across projects).
- XAML code-behind files should stay paired with their matching `.xaml` view names.

## Testing Guidelines
- There is currently no unit-test framework configured (`dotnet test` projects are not present).
- Validate changes by running the relevant executable harness under `SmugglerLib/Test/` and/or launching `MainApp`.
- Prefer adding focused verification code in the nearest harness project when introducing new behavior.

## Commit & Pull Request Guidelines
- Existing history uses short, imperative commit messages (examples: `add reference projects`, `make projects`). Keep messages concise and action-oriented.
- Recommended format: `<scope>: <imperative summary>` (example: `mainapp: add status panel binding`).
- PRs should include: purpose, affected projects/paths, local verification commands run, and screenshots for UI/XAML changes.
- Link related issues/tasks and call out configuration or dependency updates explicitly.

## Security & Configuration Tips
- Do not commit secrets in JSON/config files (for example `MessengerConfig.json`, `appsettings*.json`). Use local environment overrides.
- Treat `ThirdParty` integrations as external-boundary code; document API/credential assumptions in PR descriptions.
