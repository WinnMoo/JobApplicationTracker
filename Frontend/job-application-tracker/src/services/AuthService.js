import axios from 'axios';
import { apiURL } from '@/const.js';

class AuthService {
    login(user) {
        return axios({
            method: 'POST',
            url: `${apiURL}/account/` + 'login',
            data: {
                EmailAddress: user.emailAddress,
                Password: user.password},
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true
            }
            })
                .then(response => {
                  if (response.data.accessToken) {
                    localStorage.setItem('user', JSON.stringify(response.data))
                  }
                  return response.data;
            })
    }

    logout() {
      localStorage.removeItem('user');
    }
}

export default new AuthService();