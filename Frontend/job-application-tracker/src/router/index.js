import Vue from "vue";
import store from '../store/store.js'
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import Login from "../views/Login.vue";
import Feedback from "../views/Feedback.vue";
import Settings from "../views/Settings.vue";
import Dashboard from "../views/Dashboard.vue";
import UserSignUp from "../views/UserSignUp.vue";
import JobPostings from "../views/JobPostings.vue";
import PageNotFound from "../views/PageNotFound.vue";
import ResetPassword from "../views/ResetPassword.vue";
import ForgotPassword from "../views/ForgotPassword.vue";
import DocumentUpload from "../views/DocumentUpload.vue";
import JobApplications from "../views/JobApplication.vue";
import JobApplicationStats from "../views/JobApplicationStats.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
    meta: {
      title: 'Home'
    }
  },
  {
    path: "*",
    component: PageNotFound,
    meta: {
      title: 'Page Not Found'
    }
  },
  {
    path: "/feedback",
    name: "Feedback",
    component: Feedback,
    meta: {
      title: 'Leave Feedback',
      requiresAuth: true
    }
  },
  {
    path: "/jobapplications",
    name: "JobApplications",
    component: JobApplications,
    meta: {
      title: 'Your Saved Job Applications',
      requiresAuth: true
    }
  },
  {
    path: "/jobpostings",
    name: "JobPostings",
    component: JobPostings,
    meta: {
      title: 'Job Postings'
    }
  },
  {
    path: "/jobapplicationstats",
    name: "JobApplicationStats",
    component: JobApplicationStats,
    meta: {
      title: 'Your Job Application Statistics',
      requiresAuth: true
    }
  },
  {
    path: "/documentupload",
    name: "DocumentUpload",
    component: DocumentUpload,
    meta: {
      title: 'Upload Documents'
    }
  },
  {
    path: "/signup",
    name: "UserSignUp",
    component: UserSignUp,
    meta: {
      title: 'Sign Up'
    }
  },
  {
    path: "/login",
    name: "Login",
    component: Login,
    meta: {
      title: 'Login'
    }
  },
  {
    path: "/settings",
    name: "Settings",
    component: Settings,
    meta: {
      title: 'Settings',
      requiresAuth: true
    }
  },
  {
    path: "/forgotpassword",
    name: "ForgotPassword",
    component: ForgotPassword,
    meta: {
      title: 'Forgot Password'
    }
  },
  {
    path: "/resetpassword",
    name: "ResetPassword",
    component: ResetPassword,
    meta: {
      title: 'Reset Password'
    }
  },
  {
    path: "/dashboard",
    name: "Dashboard",
    component: Dashboard,
    meta: {
      title: 'Dashboard',
      requiresAuth: true
    }
  },
  {
    path: "/about",
    name: "About",
    meta: {
      title: 'About JobTaine'
    },
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

router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    // this route requires auth, check if logged in
    // if not, redirect to login page.
    if (!store.getters.isLoggedIn) {
      next({ name: 'Login' })
    } else {
      next() // go to wherever I'm going
    }
  } else {
    next ()
  }
})

router.afterEach((to) => {
  Vue.nextTick( () => {
    document.title = to.meta.title ? to.meta.title : 'default title';
  });
})

export default router;
