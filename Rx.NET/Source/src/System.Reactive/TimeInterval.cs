﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information. 

using System.Collections.Generic;
using System.Globalization;

namespace System.Reactive
{
    /// <summary>
    /// Represents a value associated with time interval information.
    /// The time interval can represent the time it took to produce the value, the interval relative to a previous value, the value's delivery time relative to a base, etc.
    /// </summary>
    /// <typeparam name="T">The type of the value being annotated with time interval information.</typeparam>
#if !NO_SERIALIZABLE
    [Serializable]
#endif
    public struct TimeInterval<T> : IEquatable<TimeInterval<T>>
    {
        private readonly TimeSpan _interval;
        private readonly T _value;

        /// <summary>
        /// Constructs a time interval value.
        /// </summary>
        /// <param name="value">The value to be annotated with a time interval.</param>
        /// <param name="interval">Time interval associated with the value.</param>
        public TimeInterval(T value, TimeSpan interval)
        {
            _interval = interval;
            _value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Gets the interval.
        /// </summary>
        public TimeSpan Interval
        {
            get { return _interval; }
        }

        /// <summary>
        /// Determines whether the current <see cref="TimeInterval{T}"/> value has the same Value and Interval as a specified <see cref="TimeInterval{T}"/> value.
        /// </summary>
        /// <param name="other">An object to compare to the current <see cref="TimeInterval{T}"/> value.</param>
        /// <returns>true if both <see cref="TimeInterval{T}"/> values have the same Value and Interval; otherwise, false.</returns>
        public bool Equals(TimeInterval<T> other)
        {
            return other.Interval.Equals(Interval) && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        /// <summary>
        /// Determines whether the two specified <see cref="TimeInterval{T}"/> values have the same Value and Interval.
        /// </summary>
        /// <param name="first">The first <see cref="TimeInterval{T}"/> value to compare.</param>
        /// <param name="second">The second <see cref="TimeInterval{T}"/> value to compare.</param>
        /// <returns>true if the first <see cref="TimeInterval{T}"/> value has the same Value and Interval as the second <see cref="TimeInterval{T}"/> value; otherwise, false.</returns>
        public static bool operator ==(TimeInterval<T> first, TimeInterval<T> second)
        {
            return first.Equals(second);
        }

        /// <summary>
        /// Determines whether the two specified <see cref="TimeInterval{T}"/> values don't have the same Value and Interval.
        /// </summary>
        /// <param name="first">The first <see cref="TimeInterval{T}"/> value to compare.</param>
        /// <param name="second">The second <see cref="TimeInterval{T}"/> value to compare.</param>
        /// <returns>true if the first <see cref="TimeInterval{T}"/> value has a different Value or Interval as the second <see cref="TimeInterval{T}"/> value; otherwise, false.</returns>
        public static bool operator !=(TimeInterval<T> first, TimeInterval<T> second)
        {
            return !first.Equals(second);
        }

        /// <summary>
        /// Determines whether the specified System.Object is equal to the current <see cref="TimeInterval{T}"/>.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current <see cref="TimeInterval{T}"/>.</param>
        /// <returns>true if the specified System.Object is equal to the current <see cref="TimeInterval{T}"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is TimeInterval<T>))
                return false;

            var other = (TimeInterval<T>)obj;
            return this.Equals(other);
        }

        /// <summary>
        /// Returns the hash code for the current <see cref="TimeInterval{T}"/> value.
        /// </summary>
        /// <returns>A hash code for the current <see cref="TimeInterval{T}"/> value.</returns>
        public override int GetHashCode()
        {
            var valueHashCode = Value == null ? 1963 : Value.GetHashCode();

            return Interval.GetHashCode() ^ valueHashCode;
        }

        /// <summary>
        /// Returns a string representation of the current <see cref="TimeInterval{T}"/> value.
        /// </summary>
        /// <returns>String representation of the current <see cref="TimeInterval{T}"/> value.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.CurrentCulture, "{0}@{1}", Value, Interval);
        }
    }
}