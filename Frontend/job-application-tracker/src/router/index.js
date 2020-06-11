import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import JobApplications from "../views/JobApplication.vue";
import DocumentUpload from "../views/DocumentUpload.vue";
import UserSignUp from "../views/UserSignUp.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home
  },
  {
    path: "/jobapplications",
    name: "JobApplications",
    component: JobApplications
  },
  {
    path: "/documentupload",
    name: "DocumentUpload",
    component: DocumentUpload
  },
  {
    path: "/signup",
    name: "UserSignUp",
    component: UserSignUp
  },
  {
    path: "/about",
    name: "About",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/About.vue")
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;
