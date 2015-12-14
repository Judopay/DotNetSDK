using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    // ReSharper disable UnusedMember.Global
    /// <summary>
    /// Base judo pay client that does all the CRUD operations and verifies the existence of errors on responses
    /// </summary>
    public abstract class JudoPayClient
    {
        private readonly IClient _client;
        private readonly ILog _logger;

        protected JudoPayClient(ILog logger, IClient client)
        {
            _logger = logger;
            _client = client;
        }

        /// <summary>
        /// Adds a query string parameter to a bag of query string parameters
        /// </summary>
        /// <param name="parameters">The bag.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        protected void AddParameter(Dictionary<string, string> parameters, string key, object value)
        {
            var stringValue = value == null ? String.Empty : value.ToString();

            if (!string.IsNullOrWhiteSpace(stringValue) && !parameters.ContainsKey(key))
            {
                parameters.Add(key, stringValue);
            }
        }

        /// <summary>
        /// CRUD GET
        /// </summary>
        /// <typeparam name="T">The response type</typeparam>
        /// <param name="address">The URI.</param>
        /// <param name="parameters">The query string parameters.</param>
        /// <returns>A result object that wraps the parsed response and an error if something not right happened</returns>
        protected async Task<IResult<T>> GetInternal<T>(string address,
            Dictionary<string, string> parameters = null) where T : class
        {
            T result = null;

            var response = await _client.Get<T>(address, parameters).ConfigureAwait(false);

            if (!response.ErrorResponse)
            {
                result = response.ResponseBodyObject;
            }

            return new Result<T>(result, response.JudoError);
        }

        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// CRUD CREATE
        /// </summary>
        /// <typeparam name="T">The response type</typeparam>
        /// <typeparam name="R">The body parameter type</typeparam>
        /// <param name="address">The URI.</param>
        /// <param name="entity">The body entity.</param>
        /// <param name="parameters">The query string parameters.</param>
        /// <returns>A result object that wraps the parsed response and an error if something not right happened</returns>
        protected async Task<IResult<R>> PostInternal<T, R>(string address, T entity, 
                                                                Dictionary<string, string> parameters = null)
                                                                where T : class
                                                                where R : class
        {
            R result = null;

            var response = await _client.Post<R>(address, parameters, entity).ConfigureAwait(false);

            if (!response.ErrorResponse)
            {
                result = response.ResponseBodyObject;
            }

            return new Result<R>(result, response.JudoError);
        }

        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// CRUD UPDATE
        /// </summary>
        /// <typeparam name="T">The response type</typeparam>
        /// <typeparam name="R">The body parameter type</typeparam>
        /// <param name="address">The URI.</param>
        /// <param name="entity">The body entity.</param>
        /// <param name="parameters">The query string parameters.</param>
        /// <returns>A result object that wraps the parsed response and an error if something not right happened</returns>
        protected async Task<IResult<R>> PutInternal<T, R>(string address,
                                                            T entity, 
                                                            Dictionary<string, string> parameters = null)
            where T : class
            where R : class
        {
            R result = null;

            var response = await _client.Update<R>(address, parameters, entity).ConfigureAwait(false);

            if (!response.ErrorResponse)
            {
                result = response.ResponseBodyObject;
            }

            return new Result<R>(result, response.JudoError);
        }


        /// <summary>
        /// CRUD DELETE
        /// </summary>
        /// <param name="address">The URI.</param>
        /// <param name="parameters">The query string parameters.</param>
        /// <returns>A result object that wraps the parsed response and an error if something not right happened</returns>
        protected async Task<IResult> DeleteInternal(string address, 
                                                        Dictionary<string, string> parameters = null)
        {
            var response = await _client.Delete(address, parameters).ConfigureAwait(false);

            return new Result(response.JudoError);
        }

        /// <summary>
        /// Creates the validation error message.
        /// </summary>
        /// <param name="result">The validation result.</param>
        /// <returns>An error based on validation result</returns>
        private ModelError CreateValidationErrorMessage(ValidationResult result)
		{
            if (result.IsValid)
            {
                return null;
            }

			var invalidRequestModel = new ModelError()
			    {
			        Code = (int) JudoApiError.General_Model_Error,
                    Message = "Sorry, we're unable to process your request. Please check your details and try again",
                    ModelErrors = new List<FieldError>(result.Errors.Count)
			    };

		    foreach (var validationFailure in result.Errors)
		    {
			    _logger.DebugFormat("Model validation error {0} {1}", validationFailure.PropertyName, validationFailure.ErrorMessage);
				invalidRequestModel.ModelErrors.Add(new FieldError
					{
						FieldName = validationFailure.PropertyName,
						Message = validationFailure.ErrorMessage,
                        Code = (validationFailure.ErrorCode!=null?Int32.Parse(validationFailure.ErrorCode):0),
                        Detail = string.Format( "Model validation error {0} {1}", validationFailure.PropertyName, validationFailure.ErrorMessage)
					});
			}

            return invalidRequestModel;
		}

        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// Validates the specified instance with provided validator.
        /// </summary>
        /// <typeparam name="T">The instance being validated</typeparam>
        /// <typeparam name="R">The result type of the operation that is validating the instance</typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="instance">The instance.</param>
        /// <returns>A result encapsulating the validation error or <c>null</c> if validation was successful</returns>
        protected Task<IResult<R>> Validate<T, R>(IValidator<T> validator, T instance) where R : class 
        {
            var validation = validator.Validate(instance);

            if (!validation.IsValid)
            {
                return Task.FromResult<IResult<R>>(new Result<R>(null, CreateValidationErrorMessage(validation)));
            }

            return null;
        }
    }
    // ReSharper restore UnusedMember.Global
}
