// ----------------------------------------------------------------------------
// <copyright file="IBookService.cs" company="NanoTaboada">
// Copyright (c) 2017 Nano Taboada, http://openid.nanotaboada.com.ar 
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// </copyright>
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using Dotnet.Samples.Core.Models;

namespace Dotnet.Samples.Core.Services
{
    public interface IBookService
    {
        // Create
        int Create(Book book);

        // Retrieve
        Book RetrieveByIsbn(string isbn);
        IEnumerable<Book> RetrieveAll();

        // Update
        void Update(Book book);

        // Delete
        void Delete(string isbn);

        // Validate
        bool IsValidIsbn(string isbn);
    }
}