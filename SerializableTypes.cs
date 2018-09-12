using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using SFML.Graphics;
using SFML.System;

namespace WGP
{
    namespace SERIALIZABLE
    {
        /// <summary>
        /// Used to link an ISerializable from a Transformable
        /// </summary>
        public class SerialTransformable : ISerializable
        {
            /// <summary>
            /// Base Transformable.
            /// </summary>
            public Transformable Transformable;
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("Position", Transformable.Position.ToSerializable());
                info.AddValue("Scale", Transformable.Scale.ToSerializable());
                info.AddValue("Origin", Transformable.Origin.ToSerializable());
                info.AddValue("Rotation", Transformable.Rotation);
            }
            public SerialTransformable(SerializationInfo info, StreamingContext context)
            {
                Transformable = new Transformable();
                Transformable.Position = ((SerialVector2f)info.GetValue("Position", typeof(SerialVector2f))).Vector;
                Transformable.Scale = ((SerialVector2f)info.GetValue("Scale", typeof(SerialVector2f))).Vector;
                Transformable.Origin = ((SerialVector2f)info.GetValue("Origin", typeof(SerialVector2f))).Vector;
                Transformable.Rotation = info.GetSingle("Rotation");
            }

            public SerialTransformable(Transformable transformable)
            {
                Transformable = transformable;
            }

            public SerialTransformable()
            {
            }
        }
        /// <summary>
        /// Used to link an ISerializable from a Vector2f
        /// </summary>
        public class SerialVector2f : ISerializable
        {
            /// <summary>
            /// Base Vector2f.
            /// </summary>
            public Vector2f Vector;
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("X", Vector.X);
                info.AddValue("Y", Vector.Y);
            }
            public SerialVector2f(SerializationInfo info, StreamingContext context)
            {
                Vector = new Vector2f();
                Vector.X = info.GetSingle("X");
                Vector.Y = info.GetSingle("Y");
            }

            public SerialVector2f()
            {
            }

            public SerialVector2f(Vector2f vector)
            {
                Vector = vector;
            }
        }
        /// <summary>
        /// Used to link an ISerializable from a Vector2i
        /// </summary>
        public class SerialVector2i : ISerializable
        {
            /// <summary>
            /// Base Vector2i.
            /// </summary>
            public Vector2i Vector;
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("X", Vector.X);
                info.AddValue("Y", Vector.Y);
            }
            public SerialVector2i(SerializationInfo info, StreamingContext context)
            {
                Vector = new Vector2i();
                Vector.X = info.GetInt32("X");
                Vector.Y = info.GetInt32("Y");
            }

            public SerialVector2i(Vector2i vector)
            {
                Vector = vector;
            }

            public SerialVector2i()
            {
            }
        }
        /// <summary>
        /// Used to link an ISerializable from a Vector2u
        /// </summary>
        public class SerialVector2u : ISerializable
        {
            /// <summary>
            /// Base Vector2u.
            /// </summary>
            public Vector2u Vector;
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("X", Vector.X);
                info.AddValue("Y", Vector.Y);
            }
            public SerialVector2u(SerializationInfo info, StreamingContext context)
            {
                Vector = new Vector2u();
                Vector.X = info.GetUInt32("X");
                Vector.Y = info.GetUInt32("Y");
            }

            public SerialVector2u(Vector2u vector)
            {
                Vector = vector;
            }

            public SerialVector2u()
            {
            }
        }
        /// <summary>
        /// Used to link an ISerializable from a Vector3f
        /// </summary>
        public class SerialVector3f : ISerializable
        {
            /// <summary>
            /// Base Vector2u.
            /// </summary>
            public Vector3f Vector;
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("X", Vector.X);
                info.AddValue("Y", Vector.Y);
                info.AddValue("Z", Vector.Z);
            }
            public SerialVector3f(SerializationInfo info, StreamingContext context)
            {
                Vector = new Vector3f();
                Vector.X = info.GetSingle("X");
                Vector.Y = info.GetSingle("Y");
                Vector.Z = info.GetSingle("Z");
            }

            public SerialVector3f(Vector3f vector)
            {
                Vector = vector;
            }

            public SerialVector3f()
            {
            }
        }
        /// <summary>
        /// Used to link an ISerializable from a FloatRect
        /// </summary>
        public class SerialFloatRect : ISerializable
        {
            /// <summary>
            /// Base FloatRect.
            /// </summary>
            public FloatRect Box;
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("Left", Box.Left);
                info.AddValue("Top", Box.Top);
                info.AddValue("Width", Box.Width);
                info.AddValue("Height", Box.Height);
            }
            public SerialFloatRect(SerializationInfo info, StreamingContext context)
            {
                Box = new FloatRect();
                Box.Left = info.GetSingle("Left");
                Box.Top = info.GetSingle("Top");
                Box.Height = info.GetSingle("Height");
                Box.Width = info.GetSingle("Width");
            }

            public SerialFloatRect(FloatRect box)
            {
                Box = box;
            }

            public SerialFloatRect()
            {
            }
        }
        /// <summary>
        /// Used to link an ISerializable from a IntRect
        /// </summary>
        public class SerialIntRect : ISerializable
        {
            /// <summary>
            /// Base IntRect.
            /// </summary>
            public IntRect Box;
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("Left", Box.Left);
                info.AddValue("Top", Box.Top);
                info.AddValue("Width", Box.Width);
                info.AddValue("Height", Box.Height);
            }
            public SerialIntRect(SerializationInfo info, StreamingContext context)
            {
                Box = new IntRect();
                Box.Left = info.GetInt32("Left");
                Box.Top = info.GetInt32("Top");
                Box.Height = info.GetInt32("Height");
                Box.Width = info.GetInt32("Width");
            }

            public SerialIntRect()
            {
            }

            public SerialIntRect(IntRect box)
            {
                Box = box;
            }
        }
        /// <summary>
        /// Used to link an ISerializable from a Time
        /// </summary>
        public class SerialTime : ISerializable
        {
            /// <summary>
            /// Base Time.
            /// </summary>
            public Time Time;
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("microseconds", Time.AsMicroseconds());
            }
            public SerialTime(SerializationInfo info, StreamingContext context)
            {
                Time = Time.FromMicroseconds(info.GetInt64("microseconds"));
            }

            public SerialTime()
            {
            }

            public SerialTime(Time time)
            {
                Time = time;
            }
        }
        /// <summary>
        /// Used to link an ISerializable from a Color
        /// </summary>
        public class SerialColor : ISerializable
        {
            /// <summary>
            /// Base Color.
            /// </summary>
            public Color Color;
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("R", Color.R);
                info.AddValue("G", Color.G);
                info.AddValue("B", Color.B);
                info.AddValue("A", Color.A);
            }
            public SerialColor(SerializationInfo info, StreamingContext context)
            {
                Color = new Color();
                Color.R = info.GetByte("R");
                Color.G = info.GetByte("G");
                Color.B = info.GetByte("B");
                Color.A = info.GetByte("A");
            }

            public SerialColor()
            {
            }

            public SerialColor(Color color)
            {
                Color = color;
            }
        }
    }
    public static partial class Extensions
    {
        /// <summary>
        /// Convert the base SFML type to an ISerializable variant.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Serializable value.</returns>
        public static SERIALIZABLE.SerialTransformable ToSerializable(this Transformable value) => new SERIALIZABLE.SerialTransformable(value);
        /// <summary>
        /// Convert the base SFML type to an ISerializable variant.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Serializable value.</returns>
        public static SERIALIZABLE.SerialVector2f ToSerializable(this Vector2f value) => new SERIALIZABLE.SerialVector2f(value);
        /// <summary>
        /// Convert the base SFML type to an ISerializable variant.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Serializable value.</returns>
        public static SERIALIZABLE.SerialVector3f ToSerializable(this Vector3f value) => new SERIALIZABLE.SerialVector3f(value);
        /// <summary>
        /// Convert the base SFML type to an ISerializable variant.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Serializable value.</returns>
        public static SERIALIZABLE.SerialVector2i ToSerializable(this Vector2i value) => new SERIALIZABLE.SerialVector2i(value);
        /// <summary>
        /// Convert the base SFML type to an ISerializable variant.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Serializable value.</returns>
        public static SERIALIZABLE.SerialVector2u ToSerializable(this Vector2u value) => new SERIALIZABLE.SerialVector2u(value);
        /// <summary>
        /// Convert the base SFML type to an ISerializable variant.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Serializable value.</returns>
        public static SERIALIZABLE.SerialFloatRect ToSerializable(this FloatRect value) => new SERIALIZABLE.SerialFloatRect(value);
        /// <summary>
        /// Convert the base SFML type to an ISerializable variant.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Serializable value.</returns>
        public static SERIALIZABLE.SerialIntRect ToSerializable(this IntRect value) => new SERIALIZABLE.SerialIntRect(value);
        /// <summary>
        /// Convert the base SFML type to an ISerializable variant.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Serializable value.</returns>
        public static SERIALIZABLE.SerialColor ToSerializable(this Color value) => new SERIALIZABLE.SerialColor(value);
        /// <summary>
        /// Convert the base SFML type to an ISerializable variant.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Serializable value.</returns>
        public static SERIALIZABLE.SerialTime ToSerializable(this Time value) => new SERIALIZABLE.SerialTime(value);
    }
}
