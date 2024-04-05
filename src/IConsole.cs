// ----------------------------------------------------------------------------
// <copyright file="IConsole.cs" company="L0gg3r">
// Copyright (c) L0gg3r Project
// </copyright>
// ----------------------------------------------------------------------------

using System;
using System.Globalization;

namespace L0gg3r.LogSinks.Console.Base;

/// <summary>
/// Represents a console.
/// </summary>
public interface IConsole
{
    // ┌────────────────────────────────────────────────────────────────────────────────┐
    // │ Public Properties                                                              │
    // └────────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Gets a value indicating whether the console is interactive.
    /// </summary>
    bool IsInteractive { get; }

    // ┌────────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods                                                                 │
    // └────────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Writes the specified <paramref name="message"/> to the console.
    /// </summary>
    /// <param name="message">he message to write.</param>
    /// <seealso cref="WriteLine(string)"/>
    /// <seealso cref="WriteLine()"/>
    void Write(string message);

    /// <summary>
    /// Writes the specified <paramref name="message"/> to the console followed by a line terminator.
    /// </summary>
    /// <param name="message">The message to write.</param>
    /// <seealso cref="Write(string)"/>
    /// <seealso cref="WriteLine()"/>
    void WriteLine(string message);

    /// <summary>
    /// Writes a line terminator to the console.
    /// </summary>
    /// <seealso cref="Write(string)"/>
    /// <seealso cref="WriteLine(string)"/>
    void WriteLine();

    /// <summary>
    /// Displays a confirmation prompt with the specified <paramref name="question"/> that
    /// must be answered with either 'y' or 'n'.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The method will continue to prompt the user until a valid answer is provided.
    /// </para>
    /// <para>
    /// If the console is not interactive, the method will return <see langword="true"/> as default answere immediately.
    /// </para>
    /// </remarks>
    /// <param name="question">The question to answer.</param>
    /// <returns><see langword="true"/> if the user answered 'y'; otherwise, <see langword="false"/>.</returns>
    /// <seealso cref="Confirm(string, bool)"/>
    public bool Confirm(string question) => Confirm(question, true);

    /// <summary>
    /// Displays a confirmation prompt with the specified <paramref name="question"/> that
    /// must be answered with either 'y' or 'n'.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The method will continue to prompt the user until a valid answer is provided.
    /// </para>
    /// <para>
    /// If the console is not interactive, the method will return the <paramref name="defaultAnswer"/> immediately.
    /// </para>
    /// </remarks>
    /// <param name="question">The question to answer.</param>
    /// <param name="defaultAnswer">The default answer if the console is not interactive.</param>
    /// <returns><see langword="true"/> if the user answered 'y'; otherwise, <see langword="false"/>.</returns>
    /// <seealso cref="Confirm(string)"/>
    public bool Confirm(string question, bool defaultAnswer);

    /// <summary>
    /// Asks the user the specified <paramref name="question"/> and returns the answer.
    /// </summary>
    /// <remarks>
    /// If the console is not interactive, the method will return the <see langword="default"/> value immediately.
    /// </remarks>
    /// <typeparam name="TValue">The expected <see cref="Type"/> of the answer.</typeparam>
    /// <param name="question">The question.</param>
    /// <returns>The answer as <typeparamref name="TValue"/>.</returns>
    /// <seealso cref="Ask{TValue}(string, Func{string, TValue})"/>
    /// <seealso cref="Ask{TValue}(string, Func{string, TValue}, TValue)"/>
    /// <seealso cref="Ask{TValue}(string, TValue)"/>
    TValue Ask<TValue>(string question)
    {
        static TValue Converter(string value) => (TValue)Convert.ChangeType(value, typeof(TValue), CultureInfo.InvariantCulture);

        return Ask(question, Converter);
    }

    /// <summary>
    /// Asks the user the specified <paramref name="question"/> and returns the answer.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the console is not interactive, the method will return the <see langword="default"/> value immediately.
    /// </para>
    /// <para>
    /// The method will continue to prompt the user until a valid answer is provided.
    /// </para>
    /// </remarks>
    /// <typeparam name="TValue">The expected <see cref="Type"/> of the answer.</typeparam>
    /// <param name="question">The question.</param>
    /// <param name="defaultAnswer">The default answere.</param>
    /// <returns>The answer as <typeparamref name="TValue"/>.</returns>
    /// <seealso cref="Ask{TValue}(string, Func{string, TValue})"/>
    /// <seealso cref="Ask{TValue}(string, Func{string, TValue}, TValue)"/>
    /// <seealso cref="Ask{TValue}(string)"/>
    TValue Ask<TValue>(string question, TValue defaultAnswer)
    {
        Func<string, TValue> converter = (string value) => (TValue)Convert.ChangeType(value, typeof(TValue), CultureInfo.InvariantCulture);

        return Ask(question, converter, defaultAnswer);
    }

    /// <summary>
    /// Asks the user the specified <paramref name="question"/> and returns the answer.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the console is not interactive, the method will return the <paramref name="defaultAnswer"/> immediately.
    /// </para>
    /// <para>
    /// The input will be converted to the expected <typeparamref name="TValue"/> using the provided <paramref name="converter"/>.
    /// </para>
    /// <para>
    /// The method will continue to prompt the user until a valid answer is provided.
    /// </para>
    /// </remarks>
    /// <typeparam name="TValue">The expected <see cref="Type"/> of the answer.</typeparam>
    /// <param name="question">The question.</param>
    /// <param name="converter">The converter.</param>
    /// <param name="defaultAnswer">The default answer.</param>
    /// <returns>The answer as <typeparamref name="TValue"/>.</returns>
    /// <seealso cref="Ask{TValue}(string)"/>
    /// <seealso cref="Ask{TValue}(string, TValue)"/>
    /// <seealso cref="Ask{TValue}(string, Func{string, TValue})"/>
    TValue Ask<TValue>(string question, Func<string, TValue> converter, TValue defaultAnswer);
}
