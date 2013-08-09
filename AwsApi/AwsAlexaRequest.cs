/* 
 * Copyright (C) 2013 Alex Bikfalvi
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or (at
 * your option) any later version.
 *
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.
 */

using System;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using DotNetApi.Security;
using DotNetApi.Web;
using AwsApi.Atom;
using AwsApi.Atom.Alexa;

namespace AwsApi
{
	/// <summary>
	/// A class representing the Amazon Web Services Alexa top sites service.
	/// </summary>
	public sealed class AwsAlexaRequest : AsyncWebRequest
	{
		private const string host = "ats.amazonaws.com";
		private const string action = "TopSites";
		private const string method = "GET";
		private const string signatureMethod = "HmacSHA256";
		private const int signatureVersion = 2;
		private static readonly string[] responseGroups = { 
															  "Country",
															  "City",
															  "ListCountries",
															  "ListCities"
														  };
		private static readonly string[] responseGroupNames = {
																  "Country",
																  "City",
																  "List countries",
																  "List cities"
															  };

		/// <summary>
		/// The enumeration of possible response groups.
		/// </summary>
		public enum ResponseGroup
		{
			Country = 0,
			City = 1,
			ListCountries = 2,
			ListCities = 3
		}

		/// <summary>
		/// Creates a new Alexa Top Sites request instance.
		/// </summary>
		public AwsAlexaRequest()
		{
		}

		// Public properties.

		/// <summary>
		/// Gets the host used by Alexa Top Sites request.
		/// </summary>
		public static string Host
		{
			get { return AwsAlexaRequest.host; }
		}
		/// <summary>
		/// Gets the action used by the Alexa Top Sites request.
		/// </summary>
		public static string Action
		{
			get { return AwsAlexaRequest.action; }
		}
		/// <summary>
		/// Gets the signature method used by the Alexa Top Sites request.
		/// </summary>
		public static string SignatureMethod
		{
			get { return AwsAlexaRequest.signatureMethod; }
		}
		/// <summary>
		/// Gets the signature version used by the Alexa Top Sites request.
		/// </summary>
		public static int SignatureVersion
		{
			get { return AwsAlexaRequest.signatureVersion; }
		}
		/// <summary>
		/// Gets the list of response groups used by the Alexa Top Sites request.
		/// </summary>
		public static string[] ResponseGroups
		{
			get { return AwsAlexaRequest.responseGroups; }
		}
		/// <summary>
		/// Gets the list of response group names used by the Alexa Top Sites request.
		/// </summary>
		public static string[] ResponseGroupNames
		{
			get { return AwsAlexaRequest.responseGroupNames; }
		}

		// Public methods.

		/// <summary>
		/// Begings a new asynchronous request for the Alexa Top Sites service.
		/// </summary>
		/// <param name="accessKey">The Amazon Web Services access key.</param>
		/// <param name="secretKey">The Amazon Web Services secret key.</param>
		/// <param name="countryCode">The country code. This parameter is optional. By default the request returns a global list of sites.</param>
		/// <param name="cityCode">The city code. This parameter is optional. By default the request returns a global list of sites.</param>
		/// <param name="start">The start ranking. This parameter is optional. By default the ranking starts at 1.</param>
		/// <param name="count">The number of web sites to return. Maximum is 100. By default the request returns 100 sites.</param>
		/// <param name="responseGroup">The response group.</param>
		/// <param name="callback">The callback method.</param>
		/// <param name="userState">The user state.</param>
		/// <returns></returns>
		public IAsyncResult Begin(
			SecureString accessKey,
			SecureString secretKey,
			string countryCode,
			string cityCode,
			int? start,
			int? count,
			ResponseGroup responseGroup,
			AsyncWebRequestCallback callback,
			object userState)
		{
			// Create the query for this request.
			Uri uri = this.CreateQuery(accessKey, secretKey, countryCode, cityCode, start, count, responseGroup);
			// Create the request state.
			AsyncWebResult asyncState = AsyncWebRequest.Create(uri, callback, userState);
			// Change the method.
			asyncState.Request.Method = AwsAlexaRequest.method;
			// Begin the asynchronous request.
			return this.Begin(asyncState);
		}

		/// <summary>
		/// Ends the asynchronous request and resturns the Alexa Top Sites response.
		/// </summary>
		/// <param name="result">The result of the asynchronous operation.</param>
		/// <returns>An atom object representing the Alexa Top Sites response.</returns>
		public new AtomAtsTopSitesResponse End(IAsyncResult result)
		{
			// The data string.
			string data = null;
			try
			{
				// Get the state of the asynchronous operation.
				AsyncWebResult asyncState = base.End(result, out data);
				// Parse the result data into an XML document.
				XDocument xml = XDocument.Parse(data);
				// Parse the XML document into an atom top sites response.
				return AtomAtsTopSitesResponse.Parse(xml.Root);
			}
			catch (WebException exception)
			{
				// The response atom.
				AtomResponse response = null;
				// If the data is not null.
				if (null != data)
				{
					// Parse the result data into an XML document.
					XDocument xml = XDocument.Parse(data);
					// Parse the XML document into an atom top sites response.
					response = AtomResponse.Parse(xml.Root);
				}
				// Throw a new Amazon Web Services exception.
				throw new AwsException(response, exception);
			}
		}

		// Private methods.

		private Uri CreateQuery(
			SecureString accessKey,
			SecureString secretKey,
			string countryCode,
			string cityCode,
			int? start,
			int? count,
			ResponseGroup responseGroup)
		{
			// Create the string builder used to generate the query.
			StringBuilder builderQuery = new StringBuilder();

			// Append the host, access key and action.
			builderQuery.AppendFormat("AWSAccessKeyId={0}&Action={1}", Uri.EscapeDataString(accessKey.ConvertToUnsecureString()), Uri.EscapeDataString(AwsAlexaRequest.Action));
			// Append the count value, if any.
			builderQuery.AppendFormat("&Count={0}", count.HasValue ? count.Value.ToString() : string.Empty);
			// Append the city code, if any.
			if ((null != cityCode) && (string.Empty != cityCode))
			{
				builderQuery.AppendFormat("&CityCode={0}", Uri.EscapeDataString(cityCode));
			}
			// Append the country code, if any.
			if ((null != countryCode) && (string.Empty != countryCode))
			{
				builderQuery.AppendFormat("&CountryCode={0}", Uri.EscapeDataString(countryCode));
			}
			// Append the response group.
			builderQuery.AppendFormat("&ResponseGroup={0}", Uri.EscapeDataString(AwsAlexaRequest.responseGroups[(int)responseGroup]));
			// Append the signature method.
			builderQuery.AppendFormat("&SignatureMethod={0}", Uri.EscapeDataString(AwsAlexaRequest.signatureMethod));
			// Append the signature version.
			builderQuery.AppendFormat("&SignatureVersion={0}", AwsAlexaRequest.signatureVersion);
			// Append the start value, if any.
			builderQuery.AppendFormat("&Start={0}", start.HasValue ? start.Value.ToString() : string.Empty);
			// Append the timestamp.
			builderQuery.AppendFormat("&Timestamp={0}", Uri.EscapeDataString(DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")));
			// Create the string to sign.
			string stringToSign = string.Format("{0}\n{1}\n/\n{2}", AwsAlexaRequest.method, AwsAlexaRequest.host, builderQuery.ToString());
			// Append the signature.
			builderQuery.AppendFormat("&Signature={0}", Uri.EscapeDataString(this.CreateSignature(stringToSign, secretKey)));
			// Create and return the URL for the query.
			return new Uri(string.Format("http://{0}/?{1}", AwsAlexaRequest.host, builderQuery.ToString()));
		}

		/// <summary>
		/// Creates a HMAC SHA256 signature for the specified string using the Amazon Web Services secret key.
		/// </summary>
		/// <param name="stringToSign">The string to sign.</param>
		/// <param name="secretKey">The secret key.</param>
		/// <returns>The base64 encoded signature.</returns>
		private string CreateSignature(string stringToSign, SecureString secretKey)
		{
			// Create the HMAC object.
			using (HMACSHA256 hmac = new HMACSHA256(secretKey.ConvertToUnsecureByteArray(Encoding.UTF8)))
			{
				// Return the base64 encoding hash.
				return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));
			}
		}
	}
}
