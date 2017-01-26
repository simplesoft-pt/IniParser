#region License
// The MIT License (MIT)
// 
// Copyright (c) 2016 João Simões
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion
namespace SimpleSoft.IniParser.Impl
{
    /// <summary>
    /// INI normalization options
    /// </summary>
    public class IniNormalizationOptions
    {
        /// <summary>
        /// Should empty comments be included? Defaults to <value>false</value>.
        /// </summary>
        public bool IncludeEmptyComments { get; set; } = false;

        /// <summary>
        /// Should empty sections be included? Defaults to <value>false</value>.
        /// </summary>
        public bool IncludeEmptySections { get; set; } = false;

        /// <summary>
        /// Should empty properties be included? Defaults to <value>false</value>.
        /// </summary>
        public bool IncludeEmptyProperties { get; set; } = false;

        /// <summary>
        /// Are section and property names case sensitive? Defaults to <value>false</value>.
        /// </summary>
        public bool IsCaseSensitive { get; set; } = false;

        /// <summary>
        /// Replace a duplicated property for the last found value? Defaults to <value>false</value>.
        /// </summary>
        public bool ReplaceOnDuplicatedProperties { get; set; } = false;

        /// <summary>
        /// Merge duplicated sections into a single one with all properties? Defaults to <value>false</value>.
        /// </summary>
        public bool MergeOnDuplicatedSections { get; set; } = false;

        /// <summary>
        /// Throw exceptions on errors? Defaults to <value>true</value>.
        /// </summary>
        public bool ThrowExceptions { get; set; } = true;
    }
}