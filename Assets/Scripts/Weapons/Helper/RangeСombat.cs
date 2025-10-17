using UnityEngine;

public class RangeСombat :  IBalisticAttack
{
    private bool _isCalculatingTrajectory;
    private GameObject _projectilePrefab;
    private Transform _launchPoint;
    private float _lastShootTime;
    private Vector2 _initialVelocity;
    private LineRenderer _lineRenderer;
    
    public RangeСombat(GameObject projectilePrefab, Transform launchPoint, LineRenderer lineRenderer )
    {
        _projectilePrefab = projectilePrefab;
        _launchPoint = launchPoint;
        _lineRenderer = lineRenderer;
    }
    
    /*
     * Вызывается при отжатой клавиши
     * @param LaunchProjectile
     * @return переменная isCalculatingTrajectory
     */
    public void BalisticAttack(Vector2 initialVelocity)
    {
        LaunchProjectile(initialVelocity);
    }
    
    /*
     * Выполняется выстрел снаряда
     * @param projectilePrefab, launchPoint,
     * @return
     */
    private void LaunchProjectile(Vector2 initialVelocity)
    {
        if (_projectilePrefab == null || _launchPoint == null) return;

        GameObject projectile = Object.Instantiate(_projectilePrefab, _launchPoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        
        if (rb != null)
        {
            rb.velocity = initialVelocity;
        }

        if (_lineRenderer != null)
        {
            _lineRenderer.enabled = false;
        }
    }
}