using MSharp;

namespace Model
{
    public class EmailTemplate : EntityType
    {
        public EmailTemplate()
        {
            InstanceAccessors("Recover password").LogEvents(false)
                .EnumRecords("Recover password");
            
            String("Subject").Mandatory().TrimValues(false);
            String("Key").Mandatory().TrimValues(false).Unique();
            BigString("Body", 10).Mandatory().TrimValues(false).Lines(10);
            String("Mandatory placeholders").TrimValues(false);
        }
    }
}

// In the next version (.NET Core), the M# DSL will be hosted in the C# language,
// So it will be written directly inside Visual Studio instead of this IDE.

// HOW? ------------------------------------------

// In your solution, there will be two special new projects called @M#\@Model and @M#\@UI.
// In those projects for every entity type, page and module you will define a class (similar to this).
// Every setting will be applied via a fluent method call.
// When you build those special projects, they will use MSharp.exe to generate the target code in your Domain and Website projects.

// BENEFITS ------------------------------------------

// - Free code handling: E.g. Move multiple items up & down. Cut and paste code...
// - Use the full power of C# for M# code creation:
//     > Reuse: Composition, inheritence, extension methods, ...
//     > Script: C# code snippets, code templates, ...
//     > Express: App-specific extension methods to create or configure M# things
//     > ... (get creative!)
// - Refactor: E.g. rename a type or property across all M# and custom code.
// - Documentation: Immediate Intellisense help and syntax examples for everything.
// - Roslyn: E.g. Global and app-specific code-analysis and warnings.
// - VS Extensions: E.g. special colours or icons depending on context.
// - Better navigation: E.g. F7 to switch from M# file to generated/logic code.