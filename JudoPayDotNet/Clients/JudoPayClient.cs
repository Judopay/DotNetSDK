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
    internal abstract class JudoPayClient
    {
        protected readonly IClient Client;
        protected readonly ILog Logger;

        protected JudoPayClient(ILog logger, IClient client)
        {
            Logger = logger;
            Client = client;
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

            var response = await Client.Get<T>(address, parameters).ConfigureAwait(false);

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

            var response = await Client.Post<R>(address, parameters, entity).ConfigureAwait(false);

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

            var response = await Client.Update<R>(address, parameters, entity).ConfigureAwait(false);

            if (!response.ErrorResponse)
            {
                result = response.ResponseBodyObject;
            }

            return new Result<R>(result, response.JudoError);
        }

        protected async Task<IResult> DeleteInternal<T>(string address, 
                                                        T entity, 
                                                        Dictionary<string, string> parameters = null)
            where T : class
        {
            var response = await Client.Delete(address, parameters, entity).ConfigureAwait(false);

            return new Result(response.JudoError);
        }

        protected JudoApiErrorModel CreateValidationErrorMessage(ValidationResult result)
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
			    Logger.DebugFormat("Model validation error {0} {1}", validationFailure.PropertyName, validationFailure.ErrorMessage);
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
}
