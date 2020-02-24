﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxField
{
    public partial class GameScreen : UserControl
    {

        Random randGen = new Random();

        //player1 button control keys
        Boolean leftArrowDown, rightArrowDown;

        //used to draw boxes on screen
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        //create a list to hold a column of boxes        
        List<Box> boxesLeft = new List<Box>();
        List<Box> boxesRight = new List<Box>();

        int boxSize = 20;
        int boxLeftX = 100;
        int boxGap = 200;
        int boxXOffset = 5;


        //counts to see when its time to create a new box
        int counter = 0;
        int newBoxCounter = 0;
        int patternAmount = 10;

        //create hero values
        Box hero;

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        /// <summary>
        /// Set initial game values here
        /// </summary>
        public void OnStart()
        {
            //set game start values
            Box b1 = new Box(boxLeftX, 0, boxSize);
            boxesLeft.Add(b1);

            Box b2 = new Box(boxLeftX + boxGap, 0, boxSize);
            boxesRight.Add(b2);

            newBoxCounter++;

            hero = new Box(50, 300, 20);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            //update location of all boxes (drop down screen)
            foreach (Box b in boxesLeft)
            {
                b.Fall();
            }

            foreach (Box b in boxesRight)
            {
                b.Fall();
            }

            //remove box if it has gone of screen
            if (boxesLeft[0].y > 400)
            {
                boxesLeft.RemoveAt(0);
                boxesRight.RemoveAt(0);
            }

            //add new box if it is time
            counter++;
            if (counter == 9)
            {
                newBoxCounter++;

                boxLeftX += boxXOffset;

                Box b1 = new Box(boxLeftX, 0, boxSize);
                boxesLeft.Add(b1);

                Box b2 = new Box(boxLeftX + boxGap, 0, boxSize);
                boxesRight.Add(b2);

                counter = 0;

                if (newBoxCounter == patternAmount)
                {
                    boxXOffset = -boxXOffset;
                    newBoxCounter = 0;

                    patternAmount = randGen.Next(1, 8);
                }
            }

            //move hero
            if (leftArrowDown)
            {
                hero.Move("left");
            }

            if (rightArrowDown)
            {
                hero.Move("right");
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //draw boxes to screen
            foreach (Box b in boxesLeft)
            {
                e.Graphics.FillRectangle(b.boxBrush, b.x, b.y, b.size, b.size);
            }

            foreach (Box b in boxesRight)
            {
                e.Graphics.FillRectangle(b.boxBrush, b.x, b.y, b.size, b.size);
            }

            e.Graphics.FillRectangle(whiteBrush, hero.x, hero.y, hero.size, hero.size);
        }
    }
}
