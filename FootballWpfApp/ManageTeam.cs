using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballWpfApp
{
    class ManageTeam
    {
        public List<FootBallTeam> FootBallTeamList { get; set; }
        public bool IsValidFormat { get; set; }
        public bool IsValidData { get; set; }

        /// <summary>
        /// Initialise Manager Team class and load data file.
        /// </summary>
        /// <param name="fileName"></param>
        public ManageTeam(string fileName)
        {
            string[] rawText = System.IO.File.ReadAllLines(fileName);
            string[] dataColoumns = null;
            FootBallTeam footBallTeam;
            FootBallTeamList = new List<FootBallTeam>();
            int iIndex = 0;
            foreach (var rowItem in rawText)
            {
                dataColoumns = rowItem.Split(new Char[] { '\t', ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
                if (iIndex == 0)
                {
                    iIndex++;
                    if (dataColoumns.Length != 9)
                    {
                        this.IsValidFormat = false;
                        break; // invalid format
                    }
                    this.IsValidFormat = true;
                }
                else
                {
                    if (dataColoumns.Length != 10) continue; //Skip Invalid Rows

                    footBallTeam = new FootBallTeam
                    {
                        Number = dataColoumns[0],
                        Name = dataColoumns[1],
                        P = Convert.ToInt32(dataColoumns[2]),
                        W = Convert.ToInt32(dataColoumns[3]),
                        L = Convert.ToInt32(dataColoumns[4]),
                        D = Convert.ToInt32(dataColoumns[5]),
                        For = Convert.ToInt32(dataColoumns[6]),
                        Dash = dataColoumns[7],
                        Against = Convert.ToInt32(dataColoumns[8]),
                        Points = Convert.ToInt32(dataColoumns[9])
                    };
                    FootBallTeamList.Add(footBallTeam);
                }
            }
            if (FootBallTeamList.Count > 0) this.IsValidData = true;
        }

        /// <summary>
        /// Get football team details with min diff in for and against
        /// </summary>
        /// <returns></returns>
        public FootBallTeam GetTeamWithMinDiff()
        {
            foreach (var team in FootBallTeamList)
                team.Diff = (System.Math.Abs(team.For - team.Against));

            int minDiff = FootBallTeamList.Min(team => team.Diff);
            
            return FootBallTeamList.Where(team => team.Diff == minDiff).FirstOrDefault(); 
        }
    }
}
