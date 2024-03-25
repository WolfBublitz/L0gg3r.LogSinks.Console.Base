// ----------------------------------------------------------------------------
// <copyright file="IInteractiveConsole.cs" company="WB">
// Copyright (c) WB Project
// </copyright>
// ----------------------------------------------------------------------------

using System;

namespace L0gg3r.LogSinks.Console.Base;

/// <summary>
/// An interface for an interactive console.
/// </summary>
/// <remarks>
/// An interactive console is a console that can ask questions and receive answers.
/// </remarks>
public interface IInteractiveConsole
{
    /// <summary>
    /// Displays a question and waits for confirmation with a yes or no answer.
    /// </summary>
    /// <remarks>
    /// If the user just presses enter, the default value is returned.
    /// </remarks>
    /// <param name="question">The question.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns><see langword="true"/> if the answere was yes, <see langeword="false"/> otherwise.</returns>
    bool Confirm(string question, bool defaultValue = true);

    /// <summary>
    /// Asks a question and returns the answer as <typeparamref name="TValue"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the user just presses enter, the <paramref name="defaultValue"/> is returned.
    /// </para>
    /// <para>
    /// The <paramref name="parser"/> is used to convert the user input to the <typeparamref name="TValue"/>.
    /// </para>
    /// </remarks>
    /// <typeparam name="TValue">The type of the answere.</typeparam>
    /// <param name="question">The question.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="parser">The parser.</param>
    /// <returns>The answere value.</returns>
    TValue Ask<TValue>(string question, TValue? defaultValue = default, Func<string, TValue>? parser = null);
}
