using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Rizzle
{
    class BulletQueue : Queue<Bullet>
    {
        public void AddBullet(Tower tour)
        {
            Enqueue(new Bullet(tour.towerPos));
        }
    }

    class Bullet
    {
        public Rectangle position;

        public Bullet(Rectangle position)
        {
            this.position = position;
        }

        public void destroy(BulletQueue bullet)
        {
            bullet.Dequeue();
        }

        public void target(Ennemi ennemi)
        {
            while (ennemi.ennemiPos.Y != position.Y)
                if (ennemi.ennemiPos.Y < position.Y)
                    position = new Rectangle(position.X, position.Y - 1, 0, 0);
                else
                    position = new Rectangle(position.X, position.Y + 1, 0, 0);
            while (ennemi.ennemiPos.X != position.X)
                if (ennemi.ennemiPos.X < position.X)
                    position = new Rectangle(position.X - 1, position.Y, 0, 0);
                else
                    position = new Rectangle(position.X + 1, position.Y, 0, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressources.bullet, position, Color.White);
        }
    }
}
