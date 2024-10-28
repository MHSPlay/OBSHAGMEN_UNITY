using UnityEngine;

public class BlinkingLight : MonoBehaviour {
    public static Light obj;

    public bool repeat;

    public float initialBlinkSpeed = 1f;
    public float burnoutTime = 5f;

    private float blinkSpeed;
    private float timeElapsed = 0f;

    private void Awake( ) {
        if ( obj == null )
            obj = this.gameObject.GetComponent< Light >( );

    }

    private void Update( ) {

        timeElapsed += Time.deltaTime;

        blinkSpeed = initialBlinkSpeed + ( timeElapsed * 2f );

        // random blink
        obj.intensity = Mathf.Abs( Mathf.Sin( timeElapsed * blinkSpeed ) ) * ( 1f - ( timeElapsed / burnoutTime ) );

        // lamp has die ;(
        if ( timeElapsed >= burnoutTime ) {
            obj.intensity = 0f;

            // disable
            //enabled = false;
        }

        // reset
        if ( repeat && timeElapsed >= ( burnoutTime * 2 ) ) {
            timeElapsed = 0f;
            blinkSpeed = initialBlinkSpeed;
        } 

    }

}
