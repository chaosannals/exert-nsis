import { v1 as uuidv1 } from "uuid";

/**
 * 接口套接字。
 * 
 */
export class ApiSocket {
  /**
   * 初始化。
   * 
   * @param {*} url 
   */
  constructor(url) {
    this.messages = [];
    this.invokers = {};
    this.socket = new WebSocket(url);
    this.socket.addEventListener("open", () => {
      for (let message of this.messages) {
        this.socket.send(JSON.stringify(message));
      }
      this.messages = [];
    });
    this.socket.addEventListener("message", (e) => {
      let data = JSON.parse(e.data);
      let rid = data.requestId;
      if (!(rid in this.invokers)) {
        throw Error(`找不到请求 ${rid}`);
      }

      let invoker = this.invokers[rid];
      delete this.invokers[rid];
      if (data.requestInvalid) {
        Message.error({
          message: data.requestTip,
        });
        invoker.reject(data);
      } else {
        if (data.responseTip) {
          Message.success({
            message: data.responseTip,
          });
        }
        // 当是持续响应时，提供 next 的 Promise 对象。
        if (data.responseKeep) {
          data.next = new Promise((resolve, reject) => {
            this.invokers[rid] = { resolve, reject };
          });
        }
        invoker.resolve(data);
      }
    });
  }

  /**
   * 发送信息。
   * 
   * @param {*} actor 
   * @param {*} action 
   * @param {*} data 
   * @returns 
   */
  send(actor, action, data) {
    let id = uuidv1();
    let message = {
      id,
      actor,
      action,
      data,
    };
    if (this.socket.readyState == WebSocket.OPEN) {
      this.socket.send(JSON.stringify(message));
    } else {
      this.messages.push(message);
    }
    return new Promise((resolve, reject) => {
      this.invokers[id] = { resolve, reject };
    });
  }
}

export const socket = new ApiSocket(
  `ws://${window.location.hostname}:12345/api`,
);

export default {
  install(app) {
    app.config.globalProperties.$socket = socket;
  },
};
