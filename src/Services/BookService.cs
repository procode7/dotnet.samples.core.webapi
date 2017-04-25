// ----------------------------------------------------------------------------
// <copyright file="BookService.cs" company="NanoTaboada">
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
using System.Text.RegularExpressions;
using Dotnet.Samples.Core.Models;

namespace Dotnet.Samples.Core.Services
{
    // TODO: https://blogs.msdn.microsoft.com/dotnet/2016/11/09/net-core-data-access/
    public class BookService : IBookService
    {
        private readonly int QUANTITY = 100;

        // Create
        public int Create(Book book)
        {
            return 1;
        }

        // Retrieve
        public IEnumerable<Book> RetrieveAll()
        {
            return BookBuilder.CreateCollection(QUANTITY);
        }

        public Book RetrieveByIsbn(string isbn)
        {
            return BookBuilder.CreateWithIsbn(isbn);
        }

        // Update
        public void Update(Book book)
        {
            return;
        }

        // Delete
        public void Delete(string isbn)
        {
            return;
        }

        public bool IsValidIsbn(string isbn)
        {
            // https://www.safaribooksonline.com/library/view/regular-expressions-cookbook/9781449327453/ch04s13.html
            var pattern = @"
                ^
                (?:ISBN(?:-1[03])?:?\ )?    # Optional ISBN/ISBN-10/ISBN-13 identifier.
                (?=                         # Basic format pre-checks (lookahead):
                [0-9X]{10}$                 # Require 10 digits/Xs (no separators).
                |                           # Or:
                (?=(?:[0-9]+[-\ ]){3})      # Require 3 separators
                [-\ 0-9X]{13}$              # out of 13 characters total.
                |                           # Or:
                97[89][0-9]{10}$            # 978/979 plus 10 digits (13 total).
                |                           # Or:
                (?=(?:[0-9]+[-\ ]){4})      # Require 4 separators
                [-\ 0-9]{17}$               # out of 17 characters total.
                )                           # End format pre-checks.
                (?:97[89][-\ ]?)?           # Optional ISBN-13 prefix.
                [0-9]{1,5}[-\ ]?            # 1-5 digit group identifier.
                [0-9]+[-\ ]?[0-9]+[-\ ]?    # Publisher and title identifiers.
                [0-9X]                      # Check digit.
                $
                ";
            var regex = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);

            return regex.IsMatch(isbn);
        }
    }
}