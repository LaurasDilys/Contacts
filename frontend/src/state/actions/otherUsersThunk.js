import Api from "../../domain/Api";
import { getOtherUsersAction } from "./otherUsersActions";

export const getOtherUsers = (id) => (dispatch) => {
  Api.get(`users/otherthan/${id}`)
    .then(res => {
      dispatch(getOtherUsersAction(res.data));
    });
}