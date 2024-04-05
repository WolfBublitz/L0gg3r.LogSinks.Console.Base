// ----------------------------------------------------------------------------
// <copyright file="ConsoleLogSinkBase.cs" company="L0gg3r">
// Copyright (c) L0gg3r Project
// </copyright>
// ----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace L0gg3r.LogSinks.Console.Base;

/// <summary>
/// A base class for console log sinks.
/// </summary>
/// <typeparam name="TConsole">The type of the console.</typeparam>
public abstract class ConsoleLogSinkBase<TConsole> : LogSinkBase
    where TConsole : IConsole
{
    // ┌────────────────────────────────────────────────────────────────────────────────┐
    // │ Protected Constructors                                                         │
    // └────────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleLogSinkBase{TConsole}"/> class.
    /// </summary>
    /// <param name="console">The <see cref="IConsole"/> that shall be used for writing.</param>
    protected ConsoleLogSinkBase(TConsole console)
    {
        ArgumentNullException.ThrowIfNull(console, nameof(console));

        Console = console;

        ServiceProvider.RegisterServiceInstance<IConsole>(console);
    }

    // ┌────────────────────────────────────────────────────────────────────────────────┐
    // │ Public Properties                                                              │
    // └────────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Gets the <see cref="IConsole"/> that is used for writing.
    /// </summary>
    public TConsole Console { get; }

    // ┌────────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods                                                                 │
    // └────────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Asks the user the specified <paramref name="question"/> and returns the answer asynchronously.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the console is not interactive, the method will return the <see langword="default"/> value immediately.
    /// </para>
    /// <para>
    /// The method will disable the log sink while waiting for the user input.
    /// </para>
    /// </remarks>
    /// <typeparam name="TValue">The expected <see cref="Type"/> of the answer.</typeparam>
    /// <param name="question">The question.</param>
    /// <returns>A <see cref="Task"/> delivering the answer as <typeparamref name="TValue"/>.</returns>
    /// <seealso cref="AskAsync{TValue}(string, TValue)"/>
    /// <seealso cref="AskAsync{TValue}(string, Func{string, TValue}, TValue)"/>
    public async Task<TValue> AskAsync<TValue>(string question)
    {
        await DisableAsync().ConfigureAwait(false);

        TValue answer = Console.Ask<TValue>(question);

        Enable();

        return answer;
    }

    /// <summary>
    /// Asks the user the specified <paramref name="question"/> and returns the answer asynchronously.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the console is not interactive, the method will return the <paramref name="defaultAnswer"/> immediately.
    /// </para>
    /// <para>
    /// The method will disable the log sink while waiting for the user input.
    /// </para>
    /// </remarks>
    /// <typeparam name="TValue">The expected <see cref="Type"/> of the answer.</typeparam>
    /// <param name="question">The question.</param>
    /// <param name="defaultAnswer">The default answer.</param>
    /// <returns>A <see cref="Task"/> delivering the answer as <typeparamref name="TValue"/>.</returns>
    /// <seealso cref="AskAsync{TValue}(string)"/>
    /// <seealso cref="AskAsync{TValue}(string, Func{string, TValue}, TValue)"/>
    public async Task<TValue> AskAsync<TValue>(string question, TValue defaultAnswer)
    {
        await DisableAsync().ConfigureAwait(false);

        TValue answer = Console.Ask(question, defaultAnswer);

        Enable();

        return answer;
    }

    /// <summary>
    /// Asks the user the specified <paramref name="question"/> and returns the answer asynchronously.
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
    /// <returns>A <see cref="Task"/> delivering the answer as <typeparamref name="TValue"/>.</returns>
    /// <seealso cref="AskAsync{TValue}(string)"/>
    /// <seealso cref="AskAsync{TValue}(string, TValue)"/>
    public async Task<TValue> AskAsync<TValue>(string question, Func<string, TValue> converter, TValue defaultAnswer)
    {
        await DisableAsync().ConfigureAwait(false);

        TValue answer = Console.Ask(question, converter, defaultAnswer);

        Enable();

        return answer;
    }

    /// <summary>
    /// Displays a confirmation prompt with the specified <paramref name="question"/> that
    /// must be answered with either 'y' or 'n' asynchronously.
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
    /// <returns>A <see cref="Task"/> delivering <see langword="true"/> if the user answered 'y'; otherwise, <see langword="false"/>.</returns>
    /// <seealso cref="ConfirmAsync(string, bool)"/>
    public async Task<bool> ConfirmAsync(string question)
    {
        await DisableAsync().ConfigureAwait(false);

        bool answer = Console.Confirm(question);

        Enable();

        return answer;
    }

    /// <summary>
    /// Displays a confirmation prompt with the specified <paramref name="question"/> that
    /// must be answered with either 'y' or 'n' asynchronously.
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
    /// <seealso cref="ConfirmAsync(string)"/>
    public async Task<bool> ConfirmAsync(string question, bool defaultAnswer)
    {
        await DisableAsync().ConfigureAwait(false);

        bool answer = Console.Confirm(question, defaultAnswer);

        Enable();

        return answer;
    }

    // ┌────────────────────────────────────────────────────────────────────────────────┐
    // │ Protected Methods                                                              │
    // └────────────────────────────────────────────────────────────────────────────────┘

    /// <inheritdoc/>
    /// <seealso cref="WriteAsync(in LogMessage, TConsole)"/>
    protected sealed override ValueTask WriteAsync(in LogMessage logMessage)
    {
        return WriteAsync(logMessage, Console);
    }

    /// <summary>
    /// Writes the <see cref="LogMessage"/> to the console using the <paramref name="console"/>.
    /// </summary>
    /// <param name="logMessage">The <see cref="LogMessage"/> to write.</param>
    /// <param name="console">The <see cref="IConsole"/> that shall be used for writing.</param>
    /// <returns>A <see cref="ValueTask"/> that completes when the writing has finished.</returns>
    protected abstract ValueTask WriteAsync(in LogMessage logMessage, TConsole console);
}
