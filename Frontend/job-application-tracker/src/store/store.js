import Vuex from 'vuex'
import Vue from 'vue'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    isLoggedIn: false
  },

  mutations: {
    change(state, isLoggedIn) {
      state.isLoggedIn = isLoggedIn
    }
  },

  getters: {
    isLoggedIn: state => state.isLoggedIn
  },

  actions: {
    logOut(state) {
      state.isLoggedIn = false;
    },

    logIn(state) {
      state.isLoggedIn = true;
    }
  }
})