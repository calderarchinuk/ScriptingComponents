using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Utility
{
	/// <summary>
	/// returns a position in a random direction in 1 meter
	/// </summary>
	public static Vector3 RandomPosition()
	{
		return new Vector3(Random.Range(-1f,1f),0,Random.Range(-1f,1f)).normalized;
	}

	public static Vector3 RandomTreePosition(Vector2 siteCoord)
	{
		return new Vector3(siteCoord.x + Random.Range(-40f,40f),50,siteCoord.y + Random.Range(-40f,40f));
	}

	public static Vector3 RandomPosition(float range)
	{
		return new Vector3(Random.Range(-range,range),0,Random.Range(-range,range));
	}

	public static Quaternion RandomYaw ()
	{
		return Quaternion.Euler(0,UnityEngine.Random.Range(0,360),0);
	}

	public static Color RandomColour()
	{
		return new Color(Random.Range(0.2f,1f),Random.Range(0.2f,1f),Random.Range(0.2f,1f));
	}

	public static Color RandomColour(int seed)
	{
		Random.seed = seed;
		Color c = new Color(Random.Range(0.2f,1f),Random.Range(0.2f,1f),Random.Range(0.2f,1f));
		Random.seed = System.DateTime.Now.Millisecond;
		return c;
	}

	public static bool MousePositionOverRect (Vector2 mousePosition, Rect checkRect)
	{
		if (mousePosition.x < checkRect.x){return false;}
		if (mousePosition.x > checkRect.x+checkRect.width){return false;}
		if (mousePosition.y > checkRect.y+checkRect.height){return false;}
		if (mousePosition.y < checkRect.y){return false;}
		return true;
	}

	public static int RandomSeed ()
	{
		return System.DateTime.Now.Millisecond + System.DateTime.Now.Day;
	}


	public static string ReplaceUnderscore(string input)
	{
		return input.Replace("_"," ");
	}

	public static Vector3 GetRandomNearbyGround(Vector3 position, float maxRange, float radius)
	{
		RaycastHit hit = new RaycastHit();
		for (int i = 0; i<10; i++)
		{
			if (Physics.SphereCast(position + Vector3.up * 20 + Utility.RandomPosition(maxRange),radius, Vector3.down,out hit,25,LayerMask.GetMask("Ground","EntityWall","Wall")))
			{
				if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
				{
					return hit.point;
				}
			}
		}
		return position;
	}

	public static Quaternion RandomRotation ()
	{
		return new Quaternion(Random.Range(0,1f),Random.Range(0,1f),Random.Range(0,1f),Random.Range(0,1f));
	}

	public static Vector3 GroundedPosition(Vector3 position)
	{
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(position,Vector3.down, out hit, 10, LayerMask.GetMask("Ground")))
		{
			Debug.DrawRay(hit.point,Vector3.up,Color.red,10);
			return hit.point;
		}
		return position;
	}


	public static byte[] GetBytes(string str)
	{
		byte[] bytes = new byte[str.Length * sizeof(char)];
		System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
		return bytes;
	}
	
	public static string GetString(byte[] bytes)
	{
		char[] chars = new char[bytes.Length / sizeof(char)];
		System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
		return new string(chars);
	}

	public static void GroundGizmo(Vector3 position)
	{
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(position,Vector3.down,out hit, 100))
		{
			Gizmos.DrawWireCube(hit.point,new Vector3(0.5f,0,0.5f));
			Gizmos.DrawLine(position,hit.point);
		}
	}

	public static Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float time)
	{
		Matrix4x4 ret = new Matrix4x4();
		for (int i = 0; i < 16; i++)
			ret[i] = Mathf.Lerp(from[i], to[i], time);
		return ret;
	}
}