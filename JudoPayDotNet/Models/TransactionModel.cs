using System;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Transaction information
    /// </summary>
    public class TransactionModel
    {
        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        public long TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the available date.
        /// </summary>
        /// <value>
        /// The available date.
        /// </value>
        public DateTimeOffset AvailableDate { get; set; }

        /// <summary>
        /// Has the clearing time past for this transaction, and had it already been settled
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}
