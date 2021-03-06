﻿using CommNetConstellation.CommNetLayer;
using System.Linq;
using UnityEngine;

namespace CommNetConstellation
{
    /// <summary>
    /// Data structure to be saved to the persistent.sfs
    /// </summary>
    public class Constellation
    {
        [Persistent] public short frequency;
        [Persistent] public string name;
        [Persistent] public Color color;

        /// <summary>
        /// Empty constructor for ConfigNode.LoadObjectFromConfig()
        /// </summary>
        public Constellation() { }

        /// <summary>
        /// Parameter constructor for other uses
        /// </summary>
        public Constellation(short frequency, string name, Color color)
        {
            this.frequency = frequency;
            this.name = name;
            this.color = color;
        }

        /// <summary>
        /// Retrieve the constellation color assoicated with the frequency
        /// </summary>
        public static Color getColor(int givenFreq)
        {
            Constellation possibleMatch = CNCCommNetScenario.Instance.constellations.Find(x => x.frequency == givenFreq);
            if (possibleMatch == null)
                return CNCSettings.Instance.DefaultPublicColor; // fallback color
            else
                return possibleMatch.color;
        }

        /// <summary>
        /// Retrieve the constellation name assoicated with the frequency
        /// </summary>
        public static string getName(int givenFreq)
        {
            Constellation possibleMatch = CNCCommNetScenario.Instance.constellations.Find(x => x.frequency == givenFreq);
            if (possibleMatch == null)
                return getName(CNCSettings.Instance.PublicRadioFrequency); // fallback constellation
            else
                return possibleMatch.name;
        }

        /// <summary>
        /// Sanitize the user-origin frequency prior to the commit
        /// </summary>
        public static bool isFrequencyValid(int givenFreq)
        {
            if (givenFreq < 0 || givenFreq > short.MaxValue)
                return false;

            return true;
        }

        /// <summary>
        /// Count the number of existing vessels of the given constellation
        /// </summary>
        public static int countVessels(Constellation thisConstellation)
        {
            return CNCCommNetScenario.Instance.getCommNetVessels().Sum(x => (x.getFrequencies().Contains(thisConstellation.frequency)) ? 1 : 0);
        }
    }
}
