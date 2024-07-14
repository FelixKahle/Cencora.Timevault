// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.Extensions.Azure;

/// <summary>
/// The exception that is thrown when an error occurs in the Azure Maps service.
/// </summary>
public class AzureMapsException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AzureMapsException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public AzureMapsException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AzureMapsException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public AzureMapsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}