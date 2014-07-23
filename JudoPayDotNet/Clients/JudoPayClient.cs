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
    internal abstract class JudoPayClient
    {
        private readonly IClient _client;
        private readonly ILog _logger;

        protected JudoPayClient(ILog logger, IClient client)
        {
            _logger = logger;
            _client = client;
        }

        protected void AddParameter(Dictionary<string, string> parameters, string key, object value)
        {
            var stringValue = value == null ? String.Empty : value.ToString();

            if (!string.IsNullOrEmpty(stringValue) && !parameters.ContainsKey(key))
            {
                parameters.Add(key, stringValue);
            }
        }

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


        protected async Task<IResult> DeleteInternal(string address, 
                                                        Dictionary<string, string> parameters = null)
        {
            var response = await _client.Delete(address, parameters).ConfigureAwait(false);

            return new Result(response.JudoError);
        }

        private JudoApiErrorModel CreateValidationErrorMessage(ValidationResult result)
		{
            if (result.IsValid)
            {
                return null;
            }

			var invalidRequestModel = new JudoApiErrorModel
			    {
			        ErrorType = JudoApiError.General_Model_Error,
			        ErrorMessage = "Invalid request",
                    ModelErrors = new List<JudoModelError>(result.Errors.Count)
			    };

		    foreach (var validationFailure in result.Errors)
		    {
			    _logger.DebugFormat("Model validation error {0} {1}", validationFailure.PropertyName, validationFailure.ErrorMessage);
				invalidRequestModel.ModelErrors.Add(new JudoModelError
					{
						FieldName = validationFailure.PropertyName,
						ErrorMessage = validationFailure.ErrorMessage
					});
			}

            return invalidRequestModel;
		}

        // ReSharper disable once InconsistentNaming
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
