﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace WGP
{
    /// <summary>
    /// Rendering class to build advanced SFML text with properties.
    /// </summary>
    public class RichText : Transformable, Drawable
    {
        /// <summary>
        /// A part of the final text.
        /// </summary>
        public struct Part
        {
            /// <summary>
            /// The text of the part.
            /// </summary>
            public string Text;
            /// <summary>
            /// The color of the text;
            /// </summary>
            public Color Color;
            /// <summary>
            /// The style of the text;
            /// </summary>
            public Text.Styles Style;
            internal int Identifier;
            internal bool NewLine;
        }
        private struct Hitbox
        {
            public int Identifier;
            public FloatRect Box;
        }
        private LinkedList<Part> Parts { get; set; }
        private List<Text> Buffer { get; set; }
        private List<Hitbox> Hitboxes { get; set; }
        /// <summary>
        /// The maximum width of the text. Once the text is at the maximum, it will add automatically a new line. Set to 0 or under to disable.
        /// </summary>
        public float MaxWidth { get; set; }
        /// <summary>
        /// The size of the characters.
        /// </summary>
        public uint CharacterSize { get; set; }
        /// <summary>
        /// The font used.
        /// </summary>
        public Font Font { get; set; }
        private int Index { get; set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        public RichText()
        {
            Font = null;
            CharacterSize = 0;
            Parts = new LinkedList<Part>();
            Buffer = new List<Text>();
            Hitboxes = new List<Hitbox>();
            Index = 0;
            MaxWidth = 0;
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            foreach (var text in Buffer)
            {
                target.Draw(text, states);
            }
        }
        /// <summary>
        /// Adds a part to the text.
        /// </summary>
        /// <param name="part">Part of the text to add.</param>
        public void AddPart(Part part)
        {
            Part currentPart = part;
            currentPart.Text = "";
            currentPart.Identifier = Index;
            currentPart.NewLine = false;
            foreach (var character in part.Text)
            {
                if (character == '\n')
                {
                    currentPart.NewLine = true;
                    Parts.AddLast(currentPart);
                    currentPart = part;
                    currentPart.Text = "";
                    currentPart.Identifier = Index;
                    currentPart.NewLine = false;
                    Index++;
                }
                else
                    currentPart.Text += character;
            }
            Parts.AddLast(currentPart);
            Index++;
        }
        /// <summary>
        /// Returns an index of the part pointed on by <paramref name="point"/>. Returns -1 if it's not pointing any text. 
        /// </summary>
        /// <param name="point">Point to test.</param>
        /// <returns></returns>
        public int PointOn(Vector2f point)
        {
            point = InverseTransform.TransformPoint(point);
            foreach (var box in Hitboxes)
            {
                if (box.Box.Contains(point))
                    return box.Identifier;
            }
            return -1;
        }
        /// <summary>
        /// Generates the buffer to draw the text.
        /// </summary>
        public void Generate()
        {
            if (Font == null)
                throw new Exception("No font specified.");
            Hitboxes.Clear();
            Buffer.Clear();
            Vector2f offset = new Vector2f();
            foreach (var part in Parts)
            {
                List<string> words = new List<string>();
                {
                    string tempStr = "";
                    foreach (var character in part.Text)
                    {
                        tempStr += character;
                        if (character == ' ')
                        {
                            words.Add(tempStr);
                            tempStr = "";
                        }
                    }
                    words.Add(tempStr);
                }
                int i = 0;
                foreach(var word in words)
                {
                    Text tempText = new Text();
                    tempText.Color = part.Color;
                    tempText.DisplayedString = word;
                    tempText.Style = part.Style;
                    tempText.Font = Font;
                    tempText.CharacterSize = CharacterSize;
                    if (MaxWidth <= 0)
                    {
                        tempText.Position = offset;
                        offset.X += tempText.FindCharacterPos((uint)tempText.DisplayedString.Count()).X;
                    }
                    else
                    {
                        float width = tempText.FindCharacterPos((uint)tempText.DisplayedString.Count()).X;
                        if (width + offset.X > MaxWidth)
                        {
                            offset.X = 0;
                            offset.Y += Font.GetLineSpacing(CharacterSize);
                        }
                        tempText.Position = offset;
                        offset.X += width;
                    }
                    Hitboxes.Add(new Hitbox() { Box = new FloatRect(tempText.Position, tempText.FindCharacterPos((uint)tempText.DisplayedString.Count()) + new Vector2f(0, CharacterSize)), Identifier = part.Identifier });
                    if (part.NewLine && i == words.Count - 1)
                    {
                        offset.X = 0;
                        offset.Y += Font.GetLineSpacing(CharacterSize);
                    }
                    Buffer.Add(tempText);
                    i++;
                }
            }
        }
        /// <summary>
        /// Returns the transformed bounding box.
        /// </summary>
        /// <returns>Global bounding box.</returns>
        public FloatRect GetLocalBounds()
        {
            FloatRect bounds = new FloatRect(0, 0, 0, 0);
            foreach (var box in Hitboxes)
            {
                bounds.Width = Utilities.Max(bounds.Width, box.Box.Width + box.Box.Left);
                bounds.Height = Utilities.Max(bounds.Height, box.Box.Top + box.Box.Height);
            }
            return bounds;
        }
        /// <summary>
        /// Returns the bounding box (without transformations).
        /// </summary>
        /// <returns>Bouding box.</returns>
        public FloatRect GetGlobalBounds()
        {
            return Transform.TransformRect(GetLocalBounds());
        }
        /// <summary>
        /// Empties the stored parts.
        /// </summary>
        public void Clear()
        {
            Buffer.Clear();
            Hitboxes.Clear();
            Parts.Clear();
        }
    }
}
