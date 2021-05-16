// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Naming",
    "CA1707:Identifiers should not contain underscores",
    Justification = "Underscores are used for better readability.")]

[assembly: SuppressMessage(
    "Naming",
    "CA2225:Operator overloads have named alternates",
    Justification = "Needed for implecit building in builders.")]
