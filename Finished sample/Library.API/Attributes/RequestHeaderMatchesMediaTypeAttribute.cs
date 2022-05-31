﻿using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace Library.API.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, 
        AllowMultiple = true)]
    public class RequestHeaderMatchesMediaTypeAttribute : Attribute, 
        IActionConstraint
    {
        private readonly MediaTypeCollection _mediaTypes = new ();
        private readonly string _requestHeaderToMatch;

        public RequestHeaderMatchesMediaTypeAttribute(
            string requestHeaderToMatch,
            string mediaType, params string[] otherMediaTypes)
        {
            _requestHeaderToMatch = requestHeaderToMatch
               ?? throw new ArgumentNullException(nameof(requestHeaderToMatch));

            // check if the inputted media types are valid media types
            // and add them to the _mediaTypes collection                     

            if (MediaTypeHeaderValue.TryParse(mediaType,
                out MediaTypeHeaderValue? parsedMediaType))
            {
                _mediaTypes.Add(parsedMediaType);
            }
            else
            {
                throw new ArgumentException("Argument can not be null or empty.", 
                    nameof(mediaType));
            }

            foreach (var otherMediaType in otherMediaTypes)
            {
                if (MediaTypeHeaderValue.TryParse(otherMediaType,
                   out MediaTypeHeaderValue? parsedOtherMediaType))
                {
                    _mediaTypes.Add(parsedOtherMediaType);
                }
                else
                {
                    throw new ArgumentException("Argument can not be null or empty.", 
                        nameof(otherMediaTypes));
                }
            }

        }

        public int Order { get; }

        public bool Accept(ActionConstraintContext context)
        {
            var requestHeaders = context.RouteContext.HttpContext
                .Request.Headers;
            if (!requestHeaders.ContainsKey(_requestHeaderToMatch))
            {
                return false;
            }

            var parsedRequestMediaType = new MediaType(
                requestHeaders[_requestHeaderToMatch]);

            // if one of the media types matches, return true
            foreach (var mediaType in _mediaTypes)
            {
                var parsedMediaType = new MediaType(mediaType);
                if (parsedRequestMediaType.Equals(parsedMediaType))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
