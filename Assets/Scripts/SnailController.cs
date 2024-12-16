using UnityEngine;
using Unity.Netcode;

public class SnailController : NetworkBehaviour
{
    public float moveSpeed = 1f;
    public float detectionRange = 3f;
    public Transform player;
    public ProgressBar progressBar;
    private NetworkVariable<bool> networkCakeflag = new NetworkVariable<bool>(false);

    private void Start()
    {
        // 订阅 NetworkVariable 的值变化事件
        networkCakeflag.OnValueChanged += OnCakeflagChanged;
        if (progressBar.model==1){
            setCakeflagServerRpc(true);
        }
    }

    private void OnDestroy()
    {
        // 取消订阅，防止内存泄漏
        networkCakeflag.OnValueChanged -= OnCakeflagChanged;
    }
    private void OnCakeflagChanged(bool previousValue, bool newValue)
    {
        Debug.Log("NetworkVariable changed from " + previousValue + " to " + newValue);
    }

    [ServerRpc(RequireOwnership = false)]
    private void setCakeflagServerRpc(bool cakeflag)
    {
        networkCakeflag.Value=cakeflag;
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);
        bool cakeflag = false;
        bool netBagflag = false;
        Collider cake = null;
        Collider netBag = null;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("cake"))
            {   
                cakeflag=true;
                cake= collider;
                Debug.Log("Snail found cake!");
                // loveCake(collider);
            }else if (collider.CompareTag("NetBag"))
            {

                netBagflag=true;
                netBag = collider;
                Debug.Log("Snail found netBag!");
                // runNetBag(collider);
            }
        }
        // 蜗牛失去理智
        if (cakeflag) setCakeflagServerRpc(cakeflag);
        // if (networkCakeflag.Value)
        // {        }
        if (cakeflag){
            Debug.Log("Snail loveCake!");
            loveCake(cake);
        }

        if (netBagflag){
            Debug.Log("networkCakeflag:"+networkCakeflag.Value);
            Debug.Log("Snail runNetBag!");
            runNetBag(netBag);
        }
    }
    private void runNetBag(Collider netBag)
    {
        
        // 计算蜗牛朝向玩家的方向
        Vector3 direction = (netBag.transform.position - transform.position);
        // direction.y = 0;  // 保持在水平面上
        direction.Normalize();

        // 旋转蜗牛背离玩家
        Vector3 targetDirection = new Vector3(direction.x, 0, direction.z);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        // transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

        // // 移动蜗牛
        // transform.position += -1*direction * moveSpeed * Time.deltaTime;
        // # 如果蜗牛没闻到过蛋糕
        if(!networkCakeflag.Value){
            snailRunServerRpc(Quaternion.Euler(0, targetRotation.eulerAngles.y, 0),-1*direction * moveSpeed * Time.deltaTime);
        }
        AdjustHeightToGround();
        // 计算距离
        float distance = Vector3.Distance(transform.position, netBag.transform.position);
        Debug.Log("Distance to netBag: " + distance);
        if (distance < 1f)
        {
            // 蜗牛被捕捉
            Debug.Log("Snail is caught!");
            if (progressBar!=null)
            {
                progressBar.woniu+=1;
                gameObject.SetActive(false);
            }
            // 同步蜗牛的消失
            CaptureSnailServerRpc();
            // 当前游戏对象
            // GameObject currentObject = gameObject;
            // // 销毁当前游戏对象
            // currentObject.SetActive(false);
            // // 将这个状态用netcode同步
            // // Destroy(gameObject);
        }
    }

    // ServerRpc 用于在服务器端处理捕捉逻辑
    [ServerRpc(RequireOwnership = false)]
    private void snailRunServerRpc(Quaternion rot,Vector3 deta_pos)
    {
        transform.rotation =rot;
        transform.position +=deta_pos;
        snailRunClientRpc(rot,deta_pos);
    }

    // ClientRpc 用于通知所有客户端同步蜗牛消失
    [ClientRpc]
    private void snailRunClientRpc(Quaternion rot,Vector3 deta_pos)
    {
        transform.rotation =rot;
        transform.position +=deta_pos;
    }
    // ServerRpc 用于在服务器端处理捕捉逻辑
    [ServerRpc(RequireOwnership = false)]
    private void CaptureSnailServerRpc()
    {
        // 在服务器端禁用当前对象，并通知所有客户端
        gameObject.SetActive(false);
        // 调用客户端同步消失的逻辑
        CaptureSnailClientRpc();
    }

    // ClientRpc 用于通知所有客户端同步蜗牛消失
    [ClientRpc]
    private void CaptureSnailClientRpc()
    {
        // 在所有客户端上禁用蜗牛对象
        gameObject.SetActive(false);
    }
    private void loveCake(Collider cake)
    {
        
        // 计算蜗牛朝向玩家的方向
        Vector3 direction = (cake.transform.position - transform.position);
        // direction.y = 0;  // 保持在水平面上
        direction.Normalize();

        // 旋转蜗牛朝向玩家
        Vector3 targetDirection = new Vector3(direction.x, 0, direction.z);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        // transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y + 180, 0);

        // // 移动蜗牛
        // transform.position += direction * moveSpeed * Time.deltaTime;
        snailRunServerRpc(Quaternion.Euler(0, targetRotation.eulerAngles.y + 180, 0),direction * moveSpeed * Time.deltaTime);
        AdjustHeightToGround();
        // 吃掉蛋糕
        // cake.gameObject.SetActive(false);
        float distance = Vector3.Distance(transform.position, cake.transform.position);
        if (distance < 0.3f)
        {
            Debug.Log("Snail eats cake!");
            cake.gameObject.SetActive(false);
            if (progressBar.model==3){
                // 蜗牛恢复理智
                setCakeflagServerRpc(false);
            }

        }
    }
    private void MoveTowardsPlayer()
    {
        // 计算蜗牛朝向玩家的方向
        Vector3 direction = (player.position - transform.position);
        direction.y = 0;  // 保持在水平面上
        direction.Normalize();

        // 旋转蜗牛朝向玩家
        Vector3 targetDirection = new Vector3(direction.x, 0, direction.z);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y + 180, 0);

        // 移动蜗牛
        transform.position += direction * moveSpeed * Time.deltaTime;
        AdjustHeightToGround();
    }

    private void AdjustHeightToGround()
    {
        // 使用射线检测来调整蜗牛高度
        RaycastHit hit;
        if (Physics.Raycast(transform.position + 5 * Vector3.up + Vector3.right, Vector3.down, out hit, 10f))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }
}
