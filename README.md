# Blaise CLI

CLI tool which uses our NuGet package.

Installed on VMs in our environments to aid Blaise configuration and data delivery.

## Usage

Set environment variables required in the `App.config` file, **NEVER** commit this file with populated values! If running locally it's safer to set system wide environment variables on your machine. Example:

```
setx ENV_BLAISE_SERVER_HOST_NAME=blah /m
```

Access help:

```
blaise.cli --help
```

## Creating data interface files

Blaise uses data interface files to setup its various databases. Used in the Blaise configuration pipeline.

Use the command `datainterface` with the following parameters:

| Long | Short | Description |
| --- | --- | --- |
| type | t | Type of data interface file to be created |
| file | f | File name of data interface file to be created |

Long parameter example:

```
blaise.cli datainterface --type cati --file catidb
```

Short parameter example:

```
blaise.cli datainterface -t cati -f catidb
```

 ## Running data delivery pipelines

 Extracts a questionnaires data out of Blaise for delivery. Used as part of our data delivery pipeline.

Use the command `datadelivery` with the following parameters:

| Long | Short | Description |
| --- | --- | --- |
| serverParkName | s | Name of the server park that houses the questionnaire to be delivered |
| questionnaireName | q | Name of the questionnaire to be delivered |
| file | f | File name of the questionnaire package to be delivered |
| audit | a | Option to include audit trail data as part of the delivery |
| batchSize | b | The number of cases to use for batching |

 ## Installing questionnaires

 Installs a Blaise questionnaire package into a Blaise environment. Used to install CMA packages during Blaise configuration.

Use the command `questionnaireinstall` with the following parameters:

| Long | Short | Description |
| --- | --- | --- |
| questionnaireName | q | Name of the questionnaire to be installed |
| serverParkName | s | Name of the server park to install the questionnaire |
| questionnaireFile | f | File name of the questionnaire package to be installed |

---
 
## Coding Standard Rules (C#)
 
This project uses a standardized set of formatting and naming rules to ensure consistency and maintainability in the codebase. These rules are enforced via the `.editorconfig` file.
 
The Nuget package StyleCop.Analyzers is responsible for auto code-fixing when the 'dotnet format' command is run in terminal. The extensive list of rules which this package can enforce be found here: https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/DOCUMENTATION.md
 
The editor config contains a mix of rules which only DotNet Format can understand (which the server pipeline relies on) and StyleCop.Analyzers rules which help auto code fix locally (these will have the prefix 'SA' with a number code).
 
### Formatting Rules (`*.cs`)
 
#### Indentation & Spacing
 
* **Spaces, not tabs**: `indent_style = space`
* **Indent size**: `indent_size = 4`
* **Tab width**: `tab_width = 4`
* **Final newline**: Required (`insert_final_newline = true`)
* **Trim trailing whitespace**: `trim_trailing_whitespace = true`
 
#### Line Endings
 
* **Windows-style line endings**: `end_of_line = crlf`
 
#### Curly Braces & Parentheses
 
* **Brace spacing**: Ignored (`csharp_space_between_braces = ignore`)
* **No space inside parentheses**: `csharp_space_between_parentheses = false`
 
#### Empty Lines
 
* **No multiple blank lines allowed**: `dotnet_style_allow_multiple_blank_lines = false`
 
#### Single-Line Statements
 
* **Preserve single-line formatting**: `csharp_preserve_single_line_statements = true`
 
#### Comma Spacing
 
* **Space after commas**: Yes (`dotnet_style_spacing_after_comma = true`)
* **Space before commas**: No (`dotnet_style_spacing_before_comma = false`)
 
---
 
### Miscellaneous C# Formatting
 
* **Label indentation**: Flush left (`csharp_indent_labels = flush_left`)
* **`using` directive placement**: Outside namespace (`csharp_using_directive_placement = outside_namespace:silent`)
* **Prefer simple `using` statements**: Enabled (`csharp_prefer_simple_using_statement = true:suggestion`)
* **Require braces for blocks**: Yes (`csharp_prefer_braces = true:silent`)
* **Namespace style**: Block scoped (`csharp_style_namespace_declarations = block_scoped:silent`)
* **Prefer method group conversions**: Yes (`csharp_style_prefer_method_group_conversion = true:silent`)
* **Prefer top-level statements**: Yes (`csharp_style_prefer_top_level_statements = true:silent`)
* **Prefer primary constructors**: Yes (`csharp_style_prefer_primary_constructors = true:suggestion`)
* **Prefer `System.Threading.Monitor` lock**: Yes (`csharp_prefer_system_threading_lock = true:suggestion`)
* **Expression-bodied methods**: Disabled (`csharp_style_expression_bodied_methods = false:silent`)
 
---
 
### Naming Rules (`*.{cs,vb}`)
 
#### Interfaces
 
* **Must begin with "I"**
  Rule: `interface_should_be_begins_with_i`
  Style: `IName` (PascalCase with "I" prefix)
 
#### Types (classes, structs, interfaces, enums)
 
* **Must use PascalCase**
  Rule: `types_should_be_pascal_case`
  Style: `TypeName`
 
#### Non-field Members (methods, properties, events)
 
* **Must use PascalCase**
  Rule: `non_field_members_should_be_pascal_case`
  Style: `MemberName`
 
#### Operator Placement
 
* **Operators placed at the beginning of the line when wrapping**:
  `dotnet_style_operator_placement_when_wrapping = beginning_of_line`
 
---
 
### Character Encoding
 
* **Charset**: UTF-8 (`charset = utf-8`)
 
---
 
This configuration promotes a consistent and readable codebase, aligned with modern C# conventions. All contributors should ensure their editors respect this `.editorconfig` file.