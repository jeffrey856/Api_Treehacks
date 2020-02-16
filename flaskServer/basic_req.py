import pytest

import server
import logging
import time

@pytest.fixture
def app():
    app = server.create_app()
    app.debug = True
    return app.test_client()


def test_hello_world(app):
    res = app.get("/", data=dict())
    # print(dir(res), res.status_code)

    assert res.status_code == 200
    assert b"Hello World" in res.data

def test_open_close(app):
   # print(dir(res), res.status_code)
   logging.debug("Checking for get state to return err")
   assert app.get('/open_close', query_string={"action": "get_state"}) != 200
   logging.debug("Checking for 0 state")
   assert app.get('/open_close', query_string={"action": "0"})
   logging.debug("Checking for 1 state")
   assert app.get('/open_close', query_string={"action": "1"})
   logging.debug("Checking for non 200", )
   assert app.get('/open_close', query_string={"action": "3"}) != 200
   logging.debug(app.get('/open_close', query_string={"action": "get_action"}))
   assert app.get('/open_close', query_string={"action": "get_action"})
   # logging.debug(app.get('/open_close', query_string={"action": "get_action"}))
   # assert app.get('/open_close', query_string={"action": "get_state"}) 
   #  assert res.status_code == 200
   #  assert b"Hello World" in res.data

# def test_led_open_close(app):
#    # print(dir(res), res.status_code)
#    assert app.get('/led') 
#    logging.debug("Checking for 1 state")
#    assert app.get('/open_close', query_string={"action": "1"})
#    logging.debug("Check for LED OPEN")
#    assert app.get('/led')
#    logging.debug("Checking for 0 state")
#    assert app.get('/open_close', query_string={"action": "0"})
# def check_per(app):
#     res = app.get("/foo/12345")
#     assert res.status_code == 200
#     assert b"12345" in res.data