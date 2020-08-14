import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import Login from "../components/Login.vue"
import Feedback from "../views/Feedback.vue";
import UserSignUp from "../views/UserSignUp.vue";
import Settings from "../views/Settings.vue";
import JobPostings from "../views/JobPostings.vue";
import PageNotFound from "../views/PageNotFound.vue";
import DocumentUpload from "../views/DocumentUpload.vue";
import JobApplications from "../views/JobApplication.vue";
import JobApplicationStats from "../views/JobApplicationStats.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home
  },
  {
    path: '*',
    component: PageNotFound
  },
  {
    path: "/feedback",
    name: "Feedback",
    component: Feedback
  },
  {
    path: "/jobapplications",
    name: "JobApplications",
    component: JobApplications
  },
  {
    path: "/jobpostings",
    name: "JobPostings",
    component: JobPostings
  },
  {
    path: "/jobapplicationstats",
    name: "JobApplicationStats",
    component: JobApplicationStats
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
    path: "/login",
    name: "Login",
    component: Login
  },
  {
    path: "/settings",
    name: "Settings",
    component: Settings
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
