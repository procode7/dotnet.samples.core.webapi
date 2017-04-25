// ----------------------------------------------------------------------------
// <copyright file="BookBuilder.cs" company="NanoTaboada">
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

using System;
using System.Collections.Generic;

namespace Dotnet.Samples.Core.Models
{
    public static class BookBuilder
    {
        private static Random random = new Random();

        public static Book CreateOne()
        {
            return new Book()
            {    
                Isbn = CreateRandomFakeIsbn(),
                Title = "Title1",
                Author = "Author1",
                Published = CreateRandomPastDate(),
                Publisher = "Publisher1",
                Pages = random.Next(0, 1000),
                Description = "Description1",
                InStock = Convert.ToBoolean(random.Next(0, 2))
            };
        }

        public static Book CreateWithId(int id)
        {    
            if (id < 0)
            {    
                throw new ArgumentOutOfRangeException();
            }

            return new Book()
            {    
                Isbn = CreateRandomFakeIsbn(),
                Title = "Title" + id,
                Author = "Author" + id,
                Published = CreateRandomPastDate(),
                Publisher = "Publisher" + id,
                Pages = random.Next(0, 1000),
                Description = "Description" + id,
                InStock = Convert.ToBoolean(random.Next(0, 2))
            };
        }

        public static Book CreateWithIsbn(string isbn)
        {    
            // TODO: Implement IsValidIsbn
            if (string.IsNullOrWhiteSpace(isbn))
            {    
                throw new ArgumentOutOfRangeException();
            }

            return new Book()
            {    
                Isbn = isbn,
                Title = "Title1",
                Author = "Author1",
                Published = CreateRandomPastDate(),
                Publisher = "Publisher1",
                Pages = random.Next(0, 1000),
                Description = "Description1",
                InStock = Convert.ToBoolean(random.Next(0, 2))
            };
        }

        public static IEnumerable<Book> CreateCollection(int quantity)
        {
            var books = new List<Book>();

            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                for (int i = 0; i < quantity; i++)
                {
                    books.Add(CreateWithId(i));
                }
            }
            
            return books;
        }

        private static string CreateRandomFakeIsbn()
        {
            var ean = "978";
            var group = random.Next(0, 2).ToString("0");
            var publisher = random.Next(200, 699).ToString("000");
            var title = random.Next(0, 99999).ToString("00000");
            var check = random.Next(0, 10).ToString("0"); // Not a real checksum!

            return string.Format("{0}-{1}-{2}-{3}-{4}", ean, group, publisher, title, check);
        }

        private static DateTime CreateRandomPastDate()
        {
            var start = new DateTime(1900, 1, 1);
            var range = ((TimeSpan)(DateTime.Today - start)).Days;

            return start.AddDays(random.Next(range));
        }
    }
}