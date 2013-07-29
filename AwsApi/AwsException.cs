/* 
 * Copyright (C) 2012 Alex Bikfalvi
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
using AwsApi.Atom;

namespace AwsApi
{
	/// <summary>
	/// A class representing an Amazon Web Services exception.
	/// </summary>
	public class AwsException : WebException
	{
		private AtomResponse response;

		/// <summary>
		/// Creates a new exception instance.
		/// </summary>
		/// <param name="response">The atom XML response.</param>
		/// <param name="exception">The web exception.</param>
		public AwsException(AtomResponse response, WebException exception)
			: base(exception.Message, null, exception.Status, exception.Response)
		{
			this.response = response;
		}

		/// <summary>
		/// Gets the atom response corresponding to this Amazon Web Services exception.
		/// </summary>
		public AtomResponse XmlResponse { get { return this.response; } }
	}
}
