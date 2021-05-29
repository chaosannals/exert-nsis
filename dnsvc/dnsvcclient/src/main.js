import { createApp } from "vue";
import App from "./App.vue";
import Socket from "./plugins/socket";

createApp(App)
  .use(Socket)
  .mount("#app");
