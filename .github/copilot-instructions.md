# Copilot Instructions for TaskCrony

<!-- Use this file to provide workspace-specific custom instructions to Copilot. For more details, visit https://code.visualstudio.com/docs/copilot/copilot-customization#_use-a-githubcopilotinstructionsmd-file -->

## Project Overview
TaskCrony is a Windows Forms application that automates task scheduling and file/folder management operations. The application creates BAT files and manages Windows Task Scheduler tasks for automated date-based file and folder operations.

## Key Requirements
- Target platform: Windows (.NET 8.0-windows)
- UI Framework: Windows Forms
- Task Scheduler integration using TaskScheduler library
- BAT file generation and management
- Date-based file/folder naming with configurable formats
- User-friendly GUI with checkboxes and input fields

## Architecture Guidelines
- Use Windows Forms for UI components
- Implement proper error handling for Task Scheduler operations
- Store BAT files in a "BAT" subfolder relative to the executable
- Support YYYYMMDD date formatting with configurable offset days
- Implement proper file path validation and handling

## Code Style
- Use C# naming conventions
- Implement async/await for long-running operations
- Add comprehensive error handling and user feedback
- Include XML documentation for public methods
- Follow SOLID principles for maintainable code
