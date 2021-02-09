namespace Assets.Scripts.Plugins.Features.Animations
{
    /*
 * {
 *  "Repeat": true,
 *  "Frames":
 *  [
 *      {
 *          "SpriteSheetResourceId": "PlayerSpriteSheet",
 *          "SpriteSheetResourceId": "player_1",
 *          "DurationInSeconds": 0.25
 *      },
 *      {
 *          "SpriteSheetResourceId": "PlayerSpriteSheet",
 *          "SpriteSheetResourceId": "player_2",
 *          "DurationInSeconds": 0.25
 *      },
 *      {
 *          "SpriteSheetResourceId": "PlayerSpriteSheet",
 *          "SpriteSheetResourceId": "player_3",
 *          "DurationInSeconds": 0.25
 *      },
 *      {
 *          "SpriteSheetResourceId": "PlayerSpriteSheet",
 *          "SpriteSheetResourceId": "player_1",
 *          "DurationInSeconds": 0.25
 *      }
 *  ]
 * }
 */
    public sealed class SpriteAnimationFrameDto
    {
        public string SpriteSheetResourceId { get; set; }

        public string SpriteResourceId { get; set; }

        public bool FlipVertical { get; set; }

        public bool FlipHorizontal { get; set; }

        public float? DurationInSeconds { get; set; }

        public float Red { get; set; }

        public float Green { get; set; }

        public float Blue { get; set; }

        public float Alpha { get; set; }
    }
}
